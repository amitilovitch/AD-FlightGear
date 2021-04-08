using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AD_FlightGear
{
    public class playStopButtonsVM : ViewModel
    {

        public playStopButtonsVM(ModelFG m)
        {
            model = m;

            // to check if actually needed delegate
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                notifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        // to check if actually needed - vmpause
        public bool VM_Pause
        {
            get
            {
                return model.Pause;
            }
        }

        public bool VM_Stop
        {
            get
            {
                return model.Stop;
            }
        }

        public void SetPause(bool b)
        {
            model.Pause = (b);
        }

        public void SetStop(bool b)
        {
            model.Stop = (b);
            model.defaultClock();
        }

        public void StartVideo(bool pause, bool stop)
        {
            model.Stop = stop;
            model.Pause = pause;
            model.start(model.DBflight.Length);
        }

    }
}
