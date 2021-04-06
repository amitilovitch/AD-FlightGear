using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AD_FlightGear
{

    public class VM_FlightData : INotifyPropertyChanged
    {

        private ModelFG _model;
        public VM_FlightData(ModelFG modelFG)
        {
            _model = modelFG;
            _model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public string VM_Hdg
        {
            get { return _model.Hdg; }
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
