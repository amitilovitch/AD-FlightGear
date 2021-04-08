using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
    using System.Collections.Generic;
    
    using OxyPlot;
    public class graphs_vm : INotifyPropertyChanged 
    {
        private ModelFG modelFG;
        private IList<DataPoint> vM_Points;
        public string Title { get; private set; }
        //List<DataPoint> return_list { get; set; }
        public IList<DataPoint> VM_Points 
        {
            get
            {
                return this.vM_Points;
            }
            set
            {
                vM_Points = value;
                NotifyPropertyChanged("VM_Points");
            }
        }
        public graphs_vm(ModelFG modelFG)
        {
            this.modelFG = modelFG;
            modelFG.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
            this.Title = "Data Graph";
            VM_Points = new List<DataPoint>
                        {
                        };
        }
        //public event PropertyChangedEventHandler PropChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double VM_Time
        {
            get
            {
                return modelFG.Time;
            }
        }
        
        public DBflightGear VM_dBflight
        {
            get
            {
                return modelFG.DBflight;
            }
        }
        public void DataPoints_6(int value)
        {
            
            List<DataPoint> return_list = new List<DataPoint>
            {
            };
            //modelFG.dBflight.MapDb[value]
            //return_list = null;
                for (int i = 0; i < modelFG.Time;i++)
                {
                    return_list.Add(new DataPoint(modelFG.DBflight.MapDb[value]._vectorFloat[i], i));
                }
            VM_Points = return_list;
        }
    }
}
