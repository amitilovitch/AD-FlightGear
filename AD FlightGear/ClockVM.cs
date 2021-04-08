using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AD_FlightGear
{
    public class ClockVM : ViewModel
    {

        public ClockVM(ModelFG m)
        {
            this.model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_TimeLeft
        {
            // calling to function inside model that return a string of the time passed 
            // so far
            get
            {
                return model.TimeLeft;
            }
            set
            {
                model.TimeLeft = value;
            }
        }


        public string VM_TimePassed
        {
            // calling to function inside model that return a string of the time passed 
            // so far
            get
            {
                return model.TimePassed;
            }
            set
            {
                model.TimePassed = value;
            }
        }
    }
}
