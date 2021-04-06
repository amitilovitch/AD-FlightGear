using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;


namespace AD_FlightGear
{
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
                notifyPropertyChanged("Speed");
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
        private string throttle0;
        public string Throttle0
        {
            get { return throttle0; }
            set
            {
                throttle0 = value;
                notifyPropertyChanged("Throttle");
            }
        }

        private string throttle1;
        public string Throttle1
        {
            get { return throttle1; }
            set
            {
                throttle1 = value;
                notifyPropertyChanged("Throttle");
            }
        }

        private string rudder;
        public string Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                notifyPropertyChanged("Rudder");
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
            get { return Convert.ToInt32(time /SpeedHZ); }
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
        
        //convert timeleft and timepassed to string
        public void secToClock()
        {
            int duration = DBflight.Length / 10;
            int secLeft = duration - Sec;

            TimeSpan t_passed = TimeSpan.FromSeconds(Sec);
            timePassed = t_passed.ToString(@"hh\:mm\:ss");

            TimeSpan t_left = TimeSpan.FromSeconds(secLeft);
            timeLeft = t_left.ToString(@"hh\:mm\:ss");
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
                Throttle0 = dBflight.MapDb[DBflight.Throttle0Index]._vectorFloat[time].ToString("0.0");
                Throttle1 = dBflight.MapDb[DBflight.Throttle1Index]._vectorFloat[time].ToString("0.0");
                Rudder = dBflight.MapDb[DBflight.RudderIndex]._vectorFloat[time].ToString("0.0");


                
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
                        time++;
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


        public void Initialize()
        {
            string pathCsv = @"C:\Users\azran\source\repos\AD FlightGear\AD FlightGear\reg_flight.csv";
            string pathXml =  @"playback_small.xml";
            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = pathXml;
            dBflight.InitializeDB();

            speedHZ = 1;

            start(dBflight.Length);
        }
    }
}
