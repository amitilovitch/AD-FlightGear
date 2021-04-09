using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
    public class VM_OpenFiles : INotifyPropertyChanged
    {
        private ModelFG model;

        public ModelFG Model { get; set; }
        public VM_OpenFiles(ModelFG modelFG)
        {
            model = modelFG;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string VM_PathCsv
        {
            get { return model.PathCsv; }
            set { model.PathCsv = value; }
        }

        public string VM_PathCsvReg
        {
            get { return model.PathCsvReg; }
            set { model.PathCsvReg = value; }
        }

/*        public string VM_PathDll
        {
            get { return model.PathDll; }
            set { model.PathDll = value; }
        }*/
        public void initDBreg() { model.InitializeDbReg(); }
        public void initDBrun() { model.InitializeDbRun(); }
    }
}
