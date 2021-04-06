using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
     class ModelFG : INotifyPropertyChanged
    {
        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void notifyPropertyChanged(string propName)
        {
            if (propName != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        private bool stop;
        public bool Stop
        {
            get { return stop; }
            set { stop = value; }
        }
        private bool pause;
        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        private double time;
        public double Time
        {
            get { return time; }
            set { time = value; }
        }
        private float speedHZ;
        public float SpeedHZ
        {
            get { return speedHZ; }
            set { speedHZ = value; }
        }
        private float sleepMS;
        public float SleepMS
        {
            get { return 1000/speedHZ; }
            set { sleepMS = value; }
        }

        private DBflightGear dBflight;
        public DBflightGear DBflight
        {
            get { return dBflight; }
            set { dBflight = value; }
        }
        public ModelFG()
        {
            dBflight = new DBflightGear();
        }


        public void start(int length)
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (!pause)
                    {
                        time++;
                        Thread.Sleep((int)Math.Ceiling(sleepMS));
                    } else if (time >= length)
                    {
                        break;
                    }
                    //if pause is true, time is constant
                    else { continue; }
                }
            }).Start();
        }


        public static void Main()
        {
            ModelFG model = new ModelFG();
            //dBflight = new DBflightGear();
            //DBflightGear dBflight = new DBflightGear();
            string pathCsv = @"C:\Users\azran\source\repos\AD FlightGear\AD FlightGear\reg_flight.csv";
            string pathXml =  @"playback_small.xml";
            model.dBflight._PathCsv = pathCsv;
            model.dBflight._PathXml = pathXml;
            model.dBflight.InitializeDB();


/*            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = pathXml;
            dBflight.InitializeDB();*/
            //float f = dBflight.mapDb[14]._vectorFloat[0];
            //string s = dBflight.mapDb[14]._Name;

            model.speedHZ = 10;
            model.start(model.dBflight.Length);

            //model.start(dBflight.Length);
        }
    }
}
