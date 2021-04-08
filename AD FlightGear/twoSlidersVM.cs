using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
    public class twoSlidersVM : ViewModel
    {
        public twoSlidersVM(ModelFG m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public twoSlidersVM() ///// לא בטוח שצריך אותו
        {
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        ////////////////////////////////private double VM_throttle;  XXXXXXXXXX
        public double VM_Throttle0
        {
            get { return model.Throttle0; }
            set { model.Throttle0 = value; }////??????????????? 
        }

        ////////////////////////////////private double VM_rudder;   XXXXXXXXXX
        public double VM_Rudder
        {
            get { return model.Rudder; }
            set { model.Rudder = value; }/////????????????????? 
        }

        public void VMsetModel(ModelFG newModel)
        {
            this.model = newModel;
        }

        public ModelFG M
        {
            get { return model; }
            set { this.model = value; }
        }
    }
}
