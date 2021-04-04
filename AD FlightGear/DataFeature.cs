using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    class DataFeature
    {
        string name
        {
            get; set;
        }
        string type
        {
            get; set;
        }

        public DataFeature(string nameFeature, string typeFeature)
        {
            name = nameFeature;
            type = typeFeature;
        }
    }
}
