using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;


namespace AD_FlightGear
{
    public class timeSliderViewModel : ViewModel
    {
        //private ModelFG model;

        //public event PropertyChangedEventHandler PropertyChanged;

        public timeSliderViewModel(ModelFG m)
        {
            model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        // property - scrolling right and left of the slider.
        public double VM_Time
        {
            get
            {
                return model.Time;
            }
            set
            {
                model.Time = value;
            }
        }

        public int VM_Length
        {
            get
            {
                return model.DBflight.Length;
            }
        }

        public void changeTime(double newValue)
        {
            model.Time = newValue;
        }

        public void changeClockTime()
        {
            model.secToClock();
        }
    }
}
