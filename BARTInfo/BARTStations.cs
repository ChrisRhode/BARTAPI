using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollWebServiceTest_BARTStations
{
    class BARTStations
    {

    }

    // start of JSON class created by jsonutils.com
    public class Xmlinfo
    {
        public string version { get; set; }
        public string encoding { get; set; }
    }

    public class Uri
    {
        public string cdatasection { get; set; }
    }

    public class Station
    {
        public string name { get; set; }
        public string abbr { get; set; }
        public string gtfs_latitude { get; set; }
        public string gtfs_longitude { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
    }

    public class Stations
    {
        public IList<Station> station { get; set; }
    }

    public class Root
    {
        public Uri uri { get; set; }
        public Stations stations { get; set; }
        public string message { get; set; }
    }

    public class BART_StationList
    {
        public Xmlinfo xmlinfo { get; set; }
        public Root root { get; set; }
    }

    // end of JSON class created by jsonutils.com
}

