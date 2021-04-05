using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    class ModelFG
    {

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


        public void start(int length)
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (!pause)
                    {
                        time++;
                        Thread.Sleep((int)Math.Ceiling(SleepMS));
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
            DBflightGear dBflight = new DBflightGear();
            string pathCsv = @"C:\Users\azran\source\repos\AD FlightGear\AD FlightGear\reg_flight.csv";
            string pathXml =  @"playback_small.xml";
            dBflight._PathCsv = pathCsv;
            dBflight._PathXml = pathXml;
            dBflight.InitializeDB();
            model.speedHZ = 10;




            model.start(dBflight.Length);
        }
    }
}
