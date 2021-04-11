
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

using OxyPlot;
using System.Reflection;
using System.Net.Sockets;

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

        private int length;
        public int Length {
            get { return dBflight.Length; }
            set { dBflight.Length = value;
                notifyPropertyChanged("Length");
            }
        }

        private bool isRegLoaded = false;
        public bool IsRegLoaded
        {
            get { return isRegLoaded; }
            set { isRegLoaded = value; }
        }        
        
        private bool isRunLoaded = false;
        public bool IsRunLoaded
        {
            get { return isRunLoaded; }
            set { isRunLoaded = value; }
        }

        private DBflightGear dBflight;
        public DBflightGear DBflight
        {
            get { return dBflight; }
            set { dBflight = value; }
        }

        private DBflightGear dBflightReg;

        public DBflightGear DBflightReg
        {
            get { return dBflightReg; }
            set { dBflightReg = value; }
        }
        public ModelFG()
        {
            dBflight = new DBflightGear();
            dBflightReg = new DBflightGear();
            GraphChoose = new List<DataPoint>();
            GraphCorr = new List<DataPoint>();
            GraphChooseIn = new List<DataPoint>();
            GraphCorrIn = new List<DataPoint>();
            pointsRun = new List<DataPoint>();
            pointsReg = new List<DataPoint>();
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

        private string pathCsvReg;
        public string PathCsvReg
        {
            get { return pathCsvReg; }
            set
            {
                pathCsvReg = value;
                notifyPropertyChanged("PathCsvReg");
            }
        }

        private string pathDll;
        public string PathDll
        {
            get { return pathDll; }
            set
            {
                pathDll = value;
                notifyPropertyChanged("PathDll");
                initializeDll();
            }
        }
        private dynamic c;
        public dynamic C
        {
            get { return c; }
            set
            {
                c = value;
                notifyPropertyChanged("C");
            }
        }

        public void initializeDll ()
        {
            try
            {
                Assembly dll = Assembly.LoadFile(PathDll);
                Type[] type = dll.GetExportedTypes();

                foreach (Type t in type)
                {
                    if (t.Name == "Graph_I")
                    {
                        C = Activator.CreateInstance(t);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error load dll", e);
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

        private string correlation;
        public string Correlation
        {
            get { return correlation; }
            set
            {
                correlation = value;
                notifyPropertyChanged("Correlation");
            }
        }

        private string nameCorrelation;
        public string NameCorrelation
        {
            get { return nameCorrelation; }
            set
            {
                nameCorrelation = value;
                notifyPropertyChanged("NameCorrelation");
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
                notifyAllByChooseIndex();

            }
        }
        private int corrIndex;
        public int CorrIndex
        {
            get { return dBflight.MapDb[chooseIndex].CorrIndex; }
        }
        public void notifyAllByChooseIndex()
        {
            GraphCorrIn = PointList(ListTime(), dBflight.MapDb[CorrIndex]._vectorFloat, dBflight.Length);
            GraphChooseIn = PointList(ListTime(), dBflight.MapDb[chooseIndex]._vectorFloat, dBflight.Length);
            pointsReg = PointList(dBflightReg.MapDb[chooseIndex]._vectorFloat, dBflightReg.MapDb[CorrIndex]._vectorFloat, dBflightReg.Length);
            pointsRun = PointList(dBflight.MapDb[chooseIndex]._vectorFloat, dBflight.MapDb[CorrIndex]._vectorFloat, dBflight.Length);
            Correlation = "Correaltion - " + dBflightReg.MapDb[chooseIndex].CorrResult.ToString("0.0");
            NameCorrelation = "Corrlation sensor:" + dBflightReg.MapDb[DBflightReg.MapDb[chooseIndex].Index].Name;
            //c.updateChoose(PointsRun, PointsReg, Time);
        }

        private List<DataPoint> graphChoose;
        public List<DataPoint> GraphChoose
        {
            get { return graphChoose; }
            set
            {
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

        private List<DataPoint> pointsReg;
        public List<DataPoint> PointsReg
        {
            get { return pointsReg; }
            set
            {
                pointsReg = value;
                notifyPropertyChanged("PointsChooseReg");
            }
        }
        private List<DataPoint> pointsRun;
        public List<DataPoint> PointsRun
        {
            get { return pointsRun; }
            set
            {
                graphCorrIn = value;
                notifyPropertyChanged("PointsRun");
            }
        }

        private List<Button> buttons;
        public List<Button> Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;
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
                notifyAllByTime(time);
            }
        }

        private int sec;
        public int Sec
        {
            get
            {
                sec = Convert.ToInt32(Time / 10);
                return sec;
            }
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

            // after recieving CSV file, notifying length update:
            notifyPropertyChanged("Length"); 


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
                StickX = Alieron * 35 ;
                StickY = Elevator * 35 ;

                //to graph 
                GraphCorr = GraphCorrIn.GetRange(0, Convert.ToInt32(time));
                GraphChoose = GraphChooseIn.GetRange(0, Convert.ToInt32(time));
               

                //to plugin


                //c.updatTime(time);
            }
        }
        public void start(int length)
        {
            
            ChooseIndex = 0;
            Time = 0;
            new Thread(delegate ()
            {
                var client = new TcpClient("localhost", 5400);
                var stream = client.GetStream();
                while (!stop)
                {
                    if (!pause)
                    {
                        byte[] sendbuf = Encoding.ASCII.GetBytes(dBflight._ListLine[(int)Time]);
                        stream.Write(sendbuf, 0, sendbuf.Length);
                        Time++;
                        Thread.Sleep(Convert.ToInt32(1000 / SpeedHZ));
                    }
                    if (time >= length)
                    {
                        break;
                    }
                    //if pause is true, time is constant
                    else { continue; }
                }
                stream.Close();
                client.Close();
            }).Start();
 
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

        public void copyCorrFromDBRegToDBRun()
        {
            if ((pathCsvReg != null) && (pathCsv != null)) {
                int size = dBflightReg.MapDb.Count();
                for(int i = 0; i < size; i++)
                {
                    dBflight.MapDb[i].CorrIndex = DBflightReg.MapDb[i].CorrIndex;
                    DBflight.MapDb[i].CorrResult = dBflightReg.MapDb[i].CorrResult;
                }
            }
        }
        public void initGraphs()
        {
            GraphCorrIn = PointList(ListTime(), dBflight.MapDb[dBflight.MapDb[chooseIndex].CorrIndex]._vectorFloat, dBflight.Length);
            GraphChooseIn = PointList(ListTime(), dBflight.MapDb[ChooseIndex]._vectorFloat, dBflight.Length);
        }
        public void InitializeDbReg()
        {
            //string pathCsv = @"C:\Users\Amit\source\repos\FG_2\FG_2\reg_flight.csv";
            dBflightReg._PathCsvReg = pathCsvReg;
            dBflightReg.InitializeDBreg();
            IsRegLoaded = true;
            copyCorrFromDBRegToDBRun();
        }

        public void InitializeDbRun()
        {
            //string pathCsv = @"C:\Users\Amit\source\repos\FG_2\FG_2\reg_flight.csv";
            speedHZ = 1;
            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = @"playback_small.xml";
            dBflight.InitializeDBrun();
            copyCorrFromDBRegToDBRun();
            initGraphs();
            this.defaultClock();
            this.SpeedHZ = 1;
            IsRunLoaded = true;
        }
    }
}