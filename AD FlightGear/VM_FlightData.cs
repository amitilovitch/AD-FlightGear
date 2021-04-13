using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AD_FlightGear
{
    // change
    public class VM_FlightData : INotifyPropertyChanged
    {
        private ModelFG model;
        public VM_FlightData(ModelFG modelFG)
        {
            model = modelFG;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
        public string VM_Hdg
        {
            get { return model.Hdg; }
        }
        public string VM_Speed
        {
            get { return model.Speed; }
        }
        public string VM_Alt
        {
            get { return model.Alt; }
        }
        public string VM_Pitch
        {
            get { return model.Pitch; }
        }
        public string VM_Roll
        {
            get { return model.Roll; }
        }
        public string VM_Yaw
        {
            get { return model.Yaw; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
