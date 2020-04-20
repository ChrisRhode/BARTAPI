using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// needed for Stream/StringReaders
using System.IO;
// needed for WebClient
using System.Net;
//
// added NuGet/Project Reference Newtonsoft.JSON 12.0.3 for JSON Deserialization
using Newtonsoft.Json;
namespace BARTInfo
{
    public partial class ScheduleForStation : System.Web.UI.Page
    {
        string rootURL = "http://api.bart.gov/api/";
        string APIKey = "MW9S-E7SL-26DU-VV8V";
        Dictionary<string, string> stationDictionary;
        PollWebServiceTest_BARTScheduleForStation.BART_StationSchedule infoForSelectedStation;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbStations.Visible = true;
                lbFinalDestination.Visible = false;
                lblDescription.Visible = false;
                tblDepartureTimes.Visible = false;
                loadStationList();
            }
        }

        private void loadStationList()
        {
            String cmdPortion = "stn.aspx?cmd=stns&json=y&key=" + APIKey;
            String fullURL = rootURL + cmdPortion;
            WebClient webClient = new WebClient();
            Stream response = webClient.OpenRead(fullURL);
            StreamReader responseData = new StreamReader(response);
            String responseAsString = responseData.ReadToEnd();
            response.Close();
            responseAsString = responseAsString.Replace("@", "");
            responseAsString = responseAsString.Replace("?xml", "xmlinfo");
            responseAsString = responseAsString.Replace("#cdata-section", "cdatasection");

            PollWebServiceTest_BARTStations.BART_StationList s = JsonConvert.DeserializeObject<PollWebServiceTest_BARTStations.BART_StationList>(responseAsString);

            int ndx;
            int lastNdx = s.root.stations.station.Count - 1;
            lbStations.Items.Clear();
            lbStations.Items.Add("[Pick a station]");
            stationDictionary = new Dictionary<string, string>();
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisStation = s.root.stations.station[ndx];
                stationDictionary.Add(thisStation.name, thisStation.abbr);
                lbStations.Items.Add(thisStation.name);
            }
            lbStations.SelectedIndex = 0;
            Session["BARTINFO_STATIONDICTIONARY"] = stationDictionary;
        }

        protected void lbStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedAbbr;
            String selectedName;

            if (lbStations.SelectedIndex <= 0)
            {
                return;
            }
            stationDictionary = Session["BARTINFO_STATIONDICTIONARY"] as Dictionary<string, string>;

            selectedName = lbStations.SelectedItem.ToString();
            if (!stationDictionary.TryGetValue(selectedName, out selectedAbbr))
            {
                throw (new Exception("Could not map selected Station Name to its Abbreviation"));
            }
            getScheduleForStation(selectedAbbr);

            lbFinalDestination.Visible = true;
            lblDescription.Visible = false;
            tblDepartureTimes.Visible = false;

        }

        private void getScheduleForStation(String stationAbbreviatedName)
        {

            String cmdPortion = "sched.aspx?cmd=stnsched&orig=" + stationAbbreviatedName + "&json=y&key=" + APIKey;
            String fullURL = rootURL + cmdPortion;
            
            WebClient webClient = new WebClient();
            Stream response = webClient.OpenRead(fullURL);
            StreamReader responseData = new StreamReader(response);
            String responseAsString = responseData.ReadToEnd();
            response.Close();
            responseAsString = responseAsString.Replace("@", "");
            responseAsString = responseAsString.Replace("?xml", "xmlinfo");
            responseAsString = responseAsString.Replace("#cdata-section", "cdatasection");

            Session["BARTINFO_SCHEDULEFORSTATION_TEXT"] = responseAsString;
            infoForSelectedStation = JsonConvert.DeserializeObject<PollWebServiceTest_BARTScheduleForStation.BART_StationSchedule>(responseAsString);
            
            int ndx;
            int lastNdx = infoForSelectedStation.root.station.item.Count - 1;
            
            Dictionary<string, string> destinationList = new Dictionary<string, string>();
            String unused;
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisEntry = infoForSelectedStation.root.station.item[ndx];
                if (!destinationList.TryGetValue(thisEntry.trainHeadStation, out unused))
                {
                    destinationList.Add(thisEntry.trainHeadStation, "");
                }
            }

            lbFinalDestination.Items.Clear();
            lbFinalDestination.Items.Add("[Pick a route name (train final destination)]");
            lastNdx = destinationList.Count - 1;
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                lbFinalDestination.Items.Add(destinationList.Keys.ElementAt(ndx));
            }
            lbFinalDestination.SelectedIndex = 0;
        }

        protected void lbFinalDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFinalDestination.SelectedIndex <= 0)
            {
                return;
            }

            tblDepartureTimes.Rows.Clear();

            tblDepartureTimes.BorderStyle = BorderStyle.Double;
            tblDepartureTimes.BorderWidth = 2;
            tblDepartureTimes.CellPadding = 5;
            tblDepartureTimes.CellSpacing = 5;
            tblDepartureTimes.GridLines = GridLines.Both;

            String selectedDestination = lbFinalDestination.SelectedItem.ToString();

            String responseAsString = Session["BARTINFO_SCHEDULEFORSTATION_TEXT"] as String;
            infoForSelectedStation = JsonConvert.DeserializeObject<PollWebServiceTest_BARTScheduleForStation.BART_StationSchedule>(responseAsString);

            int ndx;
            int lastNdx = infoForSelectedStation.root.station.item.Count - 1;
            
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisItem = infoForSelectedStation.root.station.item[ndx];
                if (thisItem.trainHeadStation == selectedDestination)
                {
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tc.Text = thisItem.origTime;
                    tr.Cells.Add(tc);
                    tblDepartureTimes.Rows.Add(tr);
                }
            }
            lblDescription.Visible = true;
            tblDepartureTimes.Visible = true;
        }
    }
}