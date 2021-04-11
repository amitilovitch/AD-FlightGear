using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    public class Button
    {
        private string buttonContent;
        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            {
                buttonContent = value;
            }
        }
        private string buttonID;
        public string ButtonID
        {
            get
            {
                return buttonID;
            }
            set
            {
                buttonID = value;
            }
        }
    }
}
