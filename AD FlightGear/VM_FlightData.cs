using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AD_FlightGear
{

    class VM_FlightData : INotifyPropertyChanged
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

        public double VM_Time
        {
            get { return _model.Time; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {

        }
    }
}
