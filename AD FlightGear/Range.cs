using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    class Range
    {
        private int min;
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        private int max;
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
    }
}
