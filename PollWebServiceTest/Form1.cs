using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// needed for Stream/StringReaders
using System.IO;
// needed for WebClient
using System.Net;
//
// added NuGet/Project Reference Newtonsoft.JSON 12.0.3 for JSON Deserialization
using Newtonsoft.Json;

namespace PollWebServiceTest
{
    public partial class Form1 : Form
    {
        string rootURL = "http://api.bart.gov/api/";
        string APIKey = "MW9S-E7SL-26DU-VV8V";
        Dictionary<string, string> stationDictionary;
        PollWebServiceTest_BARTScheduleForStation.BART_StationSchedule infoForSelectedStation;

        public Form1()
        {
            InitializeComponent();
        }


        private void btnFixJSON_Click(object sender, EventArgs e)
        {
            // this is a "edit as you go" routine to work with fixing/using BART API JSON data.
            // set up a call to get raw data from BART API
            // code then changes/removes illegal characters from returned data and pumps it to the output box
            // this fixed JSON in the output box can then be cut/pasted to jsonutils.com to 
            //   create a class tree for the data that can then be used with Newtonsoft.JSON to read the BART API data into the class
            //
            // must set the cmdPortion for desired data from BART API
            // Schedule for a station (used embarcadero as example)
            //String cmdPortion = "sched.aspx?cmd=stnsched&orig=embr&json=y&key=" + APIKey;
            // Known stations (abbreviations get fed as the value of "orig" to Schedule for a station
            String cmdPortion = "stn.aspx?cmd=stns&json=y&key=" + APIKey;
            //
            // ------

            String fullURL = rootURL + cmdPortion;
            WebClient webClient = new WebClient();
            Stream response = webClient.OpenRead(fullURL);
            StreamReader responseData = new StreamReader(response);
            String responseAsString = responseData.ReadToEnd();
            response.Close();
            responseAsString = responseAsString.Replace("@", "");
            responseAsString = responseAsString.Replace("?xml", "xmlinfo");
            responseAsString = responseAsString.Replace("#cdata-section", "cdatasection");
            tbOutput.Text = responseAsString;
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
            lbStationPicker.Items.Clear();
            lbStationPicker.Items.Add("[Pick a station]");
            stationDictionary = new Dictionary<string, string>();
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisStation = s.root.stations.station[ndx];
                stationDictionary.Add(thisStation.name, thisStation.abbr);
                lbStationPicker.Items.Add(thisStation.name);
            }
            lbStationPicker.SelectedIndex = 0;
        }

        // created by double clicking unused area on the form, vs using the pulldowns at the top
        private void Form1_Load(object sender, EventArgs e)
        {
            loadStationList();
        }


        private void getScheduleForStation(String stationAbbreviatedName)
        {
            
            String cmdPortion = "sched.aspx?cmd=stnsched&orig=" + stationAbbreviatedName + "&json=y&key=" + APIKey;
            String fullURL = rootURL + cmdPortion;
            lblMessage.Text = "Obtaining data ...";
            lblMessage.ForeColor = Color.Orange;
            Application.DoEvents();
            WebClient webClient = new WebClient();
            Stream response = webClient.OpenRead(fullURL);
            StreamReader responseData = new StreamReader(response);
            String responseAsString = responseData.ReadToEnd();
            response.Close();
            responseAsString = responseAsString.Replace("@", "");
            responseAsString = responseAsString.Replace("?xml", "xmlinfo");
            responseAsString = responseAsString.Replace("#cdata-section", "cdatasection");

            lblMessage.Text = "Deserializing data ...";
            Application.DoEvents();
            infoForSelectedStation = JsonConvert.DeserializeObject<PollWebServiceTest_BARTScheduleForStation.BART_StationSchedule>(responseAsString);
            lblMessage.Text = "Processing data ...";
            Application.DoEvents();
            int ndx;
            int lastNdx = infoForSelectedStation.root.station.item.Count - 1;
            tbOutput.Text = "";
            Dictionary<string, string> destinationList = new Dictionary<string, string>();
            String unused;
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisEntry = infoForSelectedStation.root.station.item[ndx];
                if(!destinationList.TryGetValue(thisEntry.trainHeadStation, out unused))
                {
                    destinationList.Add(thisEntry.trainHeadStation, "");
                }
                //tbOutput.Text += thisEntry.trainHeadStation + ": " + thisEntry.origTime + Environment.NewLine;
             }

            lbDestinationPicker.Items.Clear();
            lbDestinationPicker.Items.Add("[Pick a route]");
            lastNdx = destinationList.Count - 1;
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                lbDestinationPicker.Items.Add(destinationList.Keys.ElementAt(ndx));
            }
            lbDestinationPicker.SelectedIndex = 0;

            lblMessage.Text = "Done";
            lblMessage.ForeColor = Color.Green;
        }

        private void lbStationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedAbbr;
            String selectedName;
            
            if (lbStationPicker.SelectedIndex <= 0)
            {
                return;
            }
            selectedName = lbStationPicker.SelectedItem.ToString();
            if (!stationDictionary.TryGetValue(selectedName, out selectedAbbr))
            {
                throw (new Exception("Could not map selected Station Name to its Abbreviation"));
            }
            getScheduleForStation(selectedAbbr);
        }

        private void lbDestinationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDestinationPicker.SelectedIndex <= 0)
            {
                return;
            }
            String selectedDestination = lbDestinationPicker.SelectedItem.ToString();
            int ndx;
            int lastNdx = infoForSelectedStation.root.station.item.Count - 1;
            tbOutput.Text = "Scheduled departure times:" + Environment.NewLine;
            for (ndx = 0; ndx <= lastNdx; ndx++)
            {
                var thisItem = infoForSelectedStation.root.station.item[ndx];
                if (thisItem.trainHeadStation == selectedDestination)
                {
                    tbOutput.Text += thisItem.origTime + Environment.NewLine;
                }
            }
        }
    }

    
    
}
