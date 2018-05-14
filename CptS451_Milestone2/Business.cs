using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace milestone4
{
    class Business
    {
        public string bid { get; set; }
        public string bName { get; set; }
        public string bAddress { get; set; }
        public string bState { get; set; }
        public string bCity { get; set; }
        public double blat { get; set; }
        public double blong { get; set; }
        public string bAvgStars { get; set; }
        public string bReviewCount { get; set; }
        public string bIsOpen { get; set; }
    }
    class DBBusinessElement
    {
        //private string bId, bName, bAddress, bState, bCity;
        //private double bLatitude, bLongitude, bAvgStars;
        //private int bRevCount, numCheckins;
        //private bool bOpen;
        //private string[] bCats;
        //private string[,] bHours;
        public DBBusinessElement(NpgsqlDataReader DBReader, bool internalRead = false)
        {
            if (internalRead)
            {
                DBReader.Read();
            }
            bId = DBReader["bid"] as string;
            bName = DBReader["bname"] as string;
            bAddress = DBReader["baddress"] as string;
            bState = DBReader["bstate"] as string;
            bCity = DBReader["bcity"] as string;
            bLatitude = (Double)DBReader["blat"];
            bLongitude = (Double)DBReader["blong"];
            bAvgStars = (Double)DBReader["bavgstars"];
            bRevCount = (int)DBReader["brevcount"];
            numCheckins = (int)DBReader["numcheckins"];
            bOpen = (Boolean)DBReader["bopen"];
            bCats = DBReader["bcats"] as String[];
            bHours = DBReader["bhours"] as String[,];
        }

        public string bId { get; private set; }
        public string bName { get; private set; }
        public string bAddress { get; private set; }
        public string bState { get; private set; }
        public string bCity { get; private set; }
        public double bLatitude { get; private set; }
        public double bLongitude { get; private set; }
        public double bAvgStars { get; private set; }
        public int bRevCount { get; private set; }
        public int numCheckins { get; private set; }
        public bool bOpen { get; private set; }
        public string[] bCats { get; private set; }
        public string[,] bHours { get; private set; }
    }
}
