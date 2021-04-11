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
        }

        //private float speed;
        public string VM_Speed
        {
            get {
                float i = model.SpeedHZ/10;
                string s = i.ToString();
                return s; }
            set
            {
                //speed = value;
                if (float.Parse(value) <= 0||float.Parse(value)>99.99)
                    throw new Exception();
                model.SpeedHZ = float.Parse(value);
            }
        }
    }
}
