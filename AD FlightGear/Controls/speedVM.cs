using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    public class speedVM : ViewModel
    {
        public speedVM(ModelFG m)
        {
            this.model = m;
            //this.speed = 1;
            /// model.PropertyChanged?????
        }

        //private float speed;
        public string VM_Speed
        {
            get { return model.SpeedHZ.ToString(); }
            set
            {
                //speed = value;
                model.SpeedHZ = float.Parse(value);
            }
        }
    }
}
