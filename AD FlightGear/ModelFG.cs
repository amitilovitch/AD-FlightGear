
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
            GraphChooseIn = new List<DataPoint>();
            GraphCorrIn = new List<DataPoint>();
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
            set
            {
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


        private double stickX;
        public double StickX
        {
            get { return stickX; }
            set
            {
                this.stickX = value;
                notifyPropertyChanged("StickX");
            }
        }

        private double stickY;
        public double StickY
        {
            get { return stickY; }
            set
            {
                this.stickY = value;
                notifyPropertyChanged("StickY");
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
                GraphCorrIn = PointList(ListTime(), dBflight.MapDb[dBflight.MapDb[chooseIndex].CorrIndex]._vectorFloat, dBflight.Length);
                GraphChooseIn = PointList(ListTime(), dBflight.MapDb[chooseIndex]._vectorFloat, dBflight.Length);
            }
        }

        private List<DataPoint> graphChoose;
        public List<DataPoint> GraphChoose
        {
            get { return graphChoose; }
            set
            {
                //graphChoose = value;
                graphChoose = value;
                notifyPropertyChanged("GraphChoose");
            }
        }

        private List<DataPoint> graphChooseIn;
        public List<DataPoint> GraphChooseIn
        {
            get { return graphChooseIn; }
            set
            {
                graphChooseIn = value;
                notifyPropertyChanged("GraphChooseIn");
            }
        }
        private List<DataPoint> graphCorrIn;
        public List<DataPoint> GraphCorrIn
        {
            get { return graphCorrIn; }
            set
            {
                graphCorrIn = value;
                notifyPropertyChanged("GraphCorrIn");
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
            get { return speedHZ * 10; }
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
                Throttle0 = Convert.ToDouble(dBflight.MapDb[DBflight.Throttle0Index]._vectorFloat[time]);
                Throttle1 = Convert.ToDouble(dBflight.MapDb[DBflight.Throttle1Index]._vectorFloat[time]);
                Rudder = Convert.ToDouble(dBflight.MapDb[DBflight.RudderIndex]._vectorFloat[time]);
                Alieron = Convert.ToDouble(dBflight.MapDb[DBflight.AlieronIndex]._vectorFloat[time]);
                Elevator = Convert.ToDouble(dBflight.MapDb[DBflight.ElevatorIndex]._vectorFloat[time]);
                StickX = Alieron * 20 + 55.5;
                StickY = Elevator * 20 + 55.5;

                //to graph 
                GraphCorr = GraphCorrIn.GetRange(0, Convert.ToInt32(time));
                GraphChoose = GraphChooseIn.GetRange(0, Convert.ToInt32(time));
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

                        Thread.Sleep(Convert.ToInt32(1000 / SpeedHZ));
                    }
                    if (time >= length)
                    {
                        break;
                    }
                    //if pause is true, time is constant
                    else { continue; }
                }
            }).Start();
        }


        public void Initialize()
        {
            //string pathCsv = @"C:\Users\Amit\source\repos\FG_2\FG_2\reg_flight.csv";
            speedHZ = 1;
            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = @"playback_small.xml";
            dBflight.InitializeDB();
            initGraphs();
            this.defaultClock();

            //start(dBflight.Length); 
        }
        public List<float> ListTime()
        {
            List<float> listTime = new List<float>();
            for (int i = 0; i < dBflight.Length; i++)
            {
                listTime.Add((float)i);
            }
            return listTime;
        }


        public List<DataPoint> PointList(List<float> x, List<float> y, int size)
        {
            List<DataPoint> points = new List<DataPoint>();
            for (int i = 0; i < size; i++)
            {
                DataPoint p = new DataPoint(x[i], y[i]);
                points.Add(p);
            }
            return points;
        }
        public void initGraphs()
        {
            GraphCorrIn = PointList(ListTime(), dBflight.MapDb[dBflight.MapDb[chooseIndex].CorrIndex]._vectorFloat, dBflight.Length);
            GraphChooseIn = PointList(ListTime(), dBflight.MapDb[ChooseIndex]._vectorFloat, dBflight.Length);
        }

    }

}