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

        public string VM_Speed
        {
            get {
                float i = model.SpeedHZ/10;
                string s = i.ToString();
                return s; }
            set
            {
                if (float.Parse(value) <= 0||float.Parse(value)>6)
                    throw new Exception();
                model.SpeedHZ = float.Parse(value);
            }
        }

        public bool IsRegLoaded
        {
            get { return model.IsRegLoaded; }
            set { model.IsRegLoaded = value; }
        }

        public bool IsRunLoaded
        {
            get { return model.IsRunLoaded; }
            set { model.IsRunLoaded = value; }
        }
    }
}
