using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    class MainFG
    {
        public static void Main()
        {
            DBflightGear modelFG = new DBflightGear();
            string pathCsv = @"C:\Users\azran\source\repos\AD FlightGear\AD FlightGear\reg_flight.csv";
            string pathXml = @"playback_small.xml";
            modelFG._PathCsv = pathCsv;
            modelFG._PathXml = pathXml;
            modelFG.createDictFeature();
        }
    }
}
