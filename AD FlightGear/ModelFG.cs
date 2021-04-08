using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;


namespace AD_FlightGear
{
    using OxyPlot;
    public class ModelFG : INotifyPropertyChanged
    {
        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void notifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
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
            GraphChoose = new List<DataPoint>();
            GraphCorr = new List<DataPoint>();
            GraphPearson = new List<DataPoint>();
        }
        private string pathCsv;
        public string PathCsv
        {
            get { return pathCsv; }
            set
            {
                pathCsv = value;
                notifyPropertyChanged("PathCsv");
            }
        }

        private string pathDll;
        public string PathDll
        {
            get { return pathDll; }
            set
            {
                pathCsv = value;
                notifyPropertyChanged("PathDll");
            }
        }

        private string hdg;
        public string Hdg
        {
            get { return hdg; }
            set {
                hdg = value;
                notifyPropertyChanged("Hdg");
            }
        }

        private string speed;
        public string Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                notifyPropertyChanged("Speed");
            }
        }
        private string alt;
        public string Alt
        {
            get { return alt; }
            set
            {
                alt = value;
                notifyPropertyChanged("Alt");
            }
        }
        private string pitch;
        public string Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                notifyPropertyChanged("Pitch");
            }
        }
        private string roll;
        public string Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                notifyPropertyChanged("Roll");
            }
        }
        private string yaw;
        public string Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;
                notifyPropertyChanged("Yaw");
            }
        }
        private double throttle0;
        public double Throttle0
        {
            get { return throttle0; }
            set
            {
                throttle0 = value;
                notifyPropertyChanged("Throttle0");
            }
        }

        private double throttle1;
        public double Throttle1
        {
            get { return throttle1; }
            set
            {
                throttle1 = value;
                notifyPropertyChanged("Throttle1");
            }
        }

        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                notifyPropertyChanged("Rudder");
            }
        }

        private double alieron;
        public double Alieron
        {
            get { return alieron; }
            set
            {
                alieron = value;
                notifyPropertyChanged("Alieron");
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                notifyPropertyChanged("Elevator");
            }
        }

        private string timePassed;
        public string TimePassed
        {
            get { return timePassed; }
            set
            {
                timePassed = value;
                notifyPropertyChanged("TimePassed");
            }
        }
        private string timeLeft;
        public string TimeLeft
        {
            get { return timeLeft; }
            set
            {
                timeLeft = value;
                notifyPropertyChanged("TimeLeft");
            }
        }
        private int chooseIndex;
        public int ChooseIndex
        {
            get { return chooseIndex; }
            set
            {
                chooseIndex = value;
                notifyPropertyChanged("ChooseIndex");
                //calcCorrIndex(ListCurrentTime, DBflight.MapDb[ChooseIndex]._vectorFloat, Time);

            }
        }

        private List<DataPoint> graphChoose;
        public List<DataPoint> GraphChoose
        {
            get { return graphChoose; }
            set
            {
                graphChoose = value;
                notifyPropertyChanged("Stop");
            }
        }

        private List<DataPoint> graphCorr;
        public List<DataPoint> GraphCorr
        {
            get { return graphCorr; }
            set
            {
                graphCorr = value;
                notifyPropertyChanged("GraphCorr");
            }
        }

        private List<DataPoint> graphPearson;
        public List<DataPoint> GraphPearson
        {
            get { return graphPearson; }
            set
            {
                graphChoose = value;
                notifyPropertyChanged("GraphPearson");
            }
        }



        private bool stop;
        public bool Stop
        {
            get { return stop; }
            set 
            {
                stop = value;
                notifyPropertyChanged("Stop");
            }
        }
        private bool pause;
        public bool Pause
        {
            get { return pause; }
            set
            { 
                pause = value;
                notifyPropertyChanged("Pause");
            }
        }
        private double time;
        public double Time
        {
            get { return time; }
            set
            { 
                time = value;
                notifyPropertyChanged("Time");
            }
        }

        private int sec;
        public int Sec
        {
            get { return Convert.ToInt32(time / SpeedHZ); }
            set { sec = value; }
        }

        private float speedHZ;
        public float SpeedHZ
        {
            get { return speedHZ*10; }
            set
            {
                speedHZ = value;
                notifyPropertyChanged("SpeedHZ");
            }
        }

        public void defaultClock()
        {
            Sec = 0;
            int duration = DBflight.Length / 10;
            Time = 0;

            TimeSpan t_passed = TimeSpan.FromSeconds(sec);
            TimePassed = t_passed.ToString(@"hh\:mm\:ss");

            TimeSpan t_left = TimeSpan.FromSeconds(duration);
            TimeLeft = t_left.ToString(@"hh\:mm\:ss");
        }

        //convert timeleft and timepassed to string
        public void secToClock()
        {
            int duration = DBflight.Length / 10;
            int secLeft = duration - Sec;

            TimeSpan t_passed = TimeSpan.FromSeconds(Sec);
            TimePassed = t_passed.ToString(@"hh\:mm\:ss");

            TimeSpan t_left = TimeSpan.FromSeconds(secLeft);
            TimeLeft = t_left.ToString(@"hh\:mm\:ss");
        }
        public void notifyAllByTime(double t)
        {
            //to clock
            secToClock();


            int time = Convert.ToInt32(t);
            if (time < DBflight.Length)
            {
                //to flight data
                Hdg = dBflight.MapDb[DBflight.HdgIndex]._vectorFloat[time].ToString("0.0");
                Alt = dBflight.MapDb[DBflight.AltIndex]._vectorFloat[time].ToString("0.0");
                Speed = dBflight.MapDb[DBflight.SpeedIndex]._vectorFloat[time].ToString("0.0");
                Pitch = dBflight.MapDb[DBflight.PitchIndex]._vectorFloat[time].ToString("0.0");
                Roll = dBflight.MapDb[DBflight.RollIndex]._vectorFloat[time].ToString("0.0");
                Yaw = dBflight.MapDb[DBflight.YawIndex]._vectorFloat[time].ToString("0.0");

                //to stick
                Throttle0 =Convert.ToDouble(dBflight.MapDb[DBflight.Throttle0Index]._vectorFloat[time]);
                Throttle1 = Convert.ToDouble(dBflight.MapDb[DBflight.Throttle1Index]._vectorFloat[time]);
                Rudder = Convert.ToDouble(dBflight.MapDb[DBflight.RudderIndex]._vectorFloat[time]);
                Alieron = Convert.ToDouble(dBflight.MapDb[DBflight.AlieronIndex]._vectorFloat[time]);
                Elevator = Convert.ToDouble(dBflight.MapDb[DBflight.ElevatorIndex]._vectorFloat[time]);


                //to graph 

            }
        }
        public void start(int length)
        {
            Time = 0;
            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (!pause)
                    {
                        Time++;
                        notifyAllByTime(time);

                        Thread.Sleep(Convert.ToInt32(1000/SpeedHZ));
                    } if (time >= length)
                    {
                        break;
                    }
                    //if pause is true, time is constant
                    else { continue; }
                }
            }).Start();
        }

        public void calcCorrIndex()
        {
            int size = DBflight.MapDb.Count;
            for (int i = 0; i < size; i++)
            {
                DBflight.MapDb[i].CorrIndex = (i + 5) % (size);
            }
        }
        public void Initialize()
        {
            //string pathCsv = @"C:\Users\azran\source\repos\AD FlightGear\AD FlightGear\reg_flight.csv";
            speedHZ = 1;
            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = @"playback_small.xml";
            dBflight.InitializeDB();
            calcCorrIndex();


            start(dBflight.Length);
        }

        private List<float> listCurrentTime;
        public List<float> ListCurrentTime
        {
            get
            {
                for (int i = 0; i < time; i++)
                {
                    listCurrentTime.Add((float)i);
                }
                return listCurrentTime;
            }
        }

        public void CreateListPoint(List<Point> points , List<float> x, List<float> y, int size)
        {

            for (int i = 0; i < size; i++)
            {
                DataPoint p = new DataPoint(x[i], y[i]);
            }
        }
    }
}
