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
        public string Title { get; private set; }
        public string Title_upright { get; private set; }
        private IList<DataPoint> vM_Points;

        public IList<DataPoint> VM_Points
        {
            get
            {
                return this.vM_Points;
            }
            set
            {
                vM_Points = value;
                notifyPropertyChanged("VM_Points");
            }
        }

        public graphs_vm(ModelFG modelFG)
        {
            this.modelFG = modelFG;
            this.modelFG.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    notifyPropertyChanged("VM_" + e.PropertyName);
                };
            this.Title = "Data Graph";
            this.Title_upright = "corellation";
            VM_Points = new List<DataPoint>
            {

            };
        }
        //public event PropertyChangedEventHandler PropChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public void notifyPropertyChanged(string propName)
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
            //set { VM_C.updateTime(VM_TimeInt); }
        }
        public int VM_TimeInt
        {
            get
            {
                return (Convert.ToInt32(modelFG.Time));
            }
        }

        public DBflightGear VM_DBflight
        {
            get
            {
                return modelFG.DBflight;
            }
        }
        public List<Button> VM_Buttons
        {
            get
            {
                return modelFG.Buttons;
            }
        }

/*        public List<DataPoint> VM_GreyPoints
        {
            get
            {
                return modelFG.Ad.GreyPoints;
            }
        }
        public List<DataPoint> VM_BluePoints
        {
            get
            {
                return modelFG.Ad.BluePoints;
            }
        }

        public List<DataPoint> VM_RedPoints
        {
            get
            {
                return modelFG.Ad.RedPoints;
            }
        }

        public List<DataPoint> VM_ListPointCircle
        {
            get
            {
                return modelFG.Ad.ListPointLine;
            }
        }*/

        /*
        public IList<DataPoint> VM_GraphPearson
        {
            get
            {
                return modelFG.GraphPearson;
            }
        }
        */
        public int VM_ChooseIndex
        {
            get
            {
                return modelFG.ChooseIndex;
            }
            set
            {
                modelFG.ChooseIndex = value;
                notifyPropertyChanged("VM_ChooseIndex");
            }
        }

        public IList<DataPoint> VM_GraphCorr
        {
            get
            {
                return modelFG.GraphCorr;
            }
        }
        public IList<DataPoint> VM_GraphChoose
        {
            get
            {
                return modelFG.GraphChoose;
            }
        }


        public List<DataPoint> VM_PointsReg
        {
            get
            {
                return modelFG.PointsReg;
            }
        }
        public List<DataPoint> VM_PointsRun
        {
            get
            {
                return modelFG.PointsRun;
            }
        }
        //חדש
        public string VM_PathDll
        {
            get { return modelFG.PathDll; }
            set
            {
                modelFG.PathDll = value;
                notifyPropertyChanged("VM_PathDll");
            }
        }

        public string VM_PathCsv
        {
            get { return modelFG.PathCsv; }
            set { modelFG.PathCsv = value; }
        }

        public string VM_PathCsvReg
        {
            get { return modelFG.PathCsvReg; }
            set { modelFG.PathCsvReg = value; }
        }
        //חדש
        public dynamic VM_C
        {
            get { return modelFG.C; }
            set
            {
                modelFG.C = value;
                notifyPropertyChanged("VM_C");
            }
        }
        public string VM_Correlation
        {
            get { return modelFG.Correlation; }
            set
            {
                modelFG.Correlation = value;
                notifyPropertyChanged("VM_Correlation");
            }          
        }
        public string VM_NameCorrelation
        {
            get { return modelFG.NameCorrelation; }
            set
            {
                modelFG.NameCorrelation = value;
                notifyPropertyChanged("VM_NameCorrelation");
            }
        }


        public void initDBreg() { modelFG.InitializeDbReg(); }
        public void initDBrun() { modelFG.InitializeDbRun(); }
        public void DataPoints_6(int value)
        {

            List<DataPoint> return_list = new List<DataPoint>
            {
            };

            for (int i = 0; i < modelFG.Time; i++)
            {
                return_list.Add(new DataPoint(i, modelFG.DBflight.MapDb[value]._vectorFloat[i]));
            }
            VM_Points = return_list;
        }
    }
}
