using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
    public class joystickVM : ViewModel
    {
        public joystickVM(ModelFG model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public double VM_StickX
        {
            get { return model.StickX; }
            set { model.StickX = value; }
        }
        public double VM_StickY
        {
            get { return model.StickY; }
            set { model.StickY = value; }
        }

        public double VM_Alieron
        {
            get { return model.Alieron; }
            set { model.Alieron = value; }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
            set { model.Elevator = value; }
        }
    }
}
