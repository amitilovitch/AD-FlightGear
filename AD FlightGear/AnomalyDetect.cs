using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    public class AnomalyDetect
    {

        //lists according colors
        private List<DataPoint> greyPoints;
        public List<DataPoint> GreyPoints { get; set; }

        private List<DataPoint> bluePoints;
        public List<DataPoint> BluePoints { get; set; }

        private List<DataPoint> redPoints;
        public List<DataPoint> RedPoints { get; set; }

        //circle
        public List<DataPoint> listPointCircle;
        public List<DataPoint> ListPointCircle { get; set; }

        private float radius;
        public float Radius { get; set; }

        private MinCircle minCircle;





        //lists point from app

        private List<DataPoint> regPoints;
        public List<DataPoint> RegPoints { get; set; }
        private List<DataPoint> runPoints;

        public List<DataPoint> RunPoints { get; set; }




        private int time;
        public int Time { get; set; }


        private int oldTime;
        public int OldTime { get; set; }
        private double treshold;
        public double Treshold { get; set; }
        public AnomalyDetect()
        {
            this.GreyPoints = new List<DataPoint>();
            this.BluePoints = new List<DataPoint>();
            this.RedPoints = new List<DataPoint>();

            this.listPointCircle = new List<DataPoint>();
            this.minCircle = new MinCircle();

            this.RegPoints = new List<DataPoint>();
            this.RunPoints = new List<DataPoint>();
            oldTime = 0;
        }

        public void initMinCircle()
        {
            radius = this.minCircle.C.Radius;
            ListPointCircle[0] = minCircle.C.Center;
        }


        public float distance(DataPoint p1, DataPoint p2)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
        }
        //true is detect anomaly
        public bool detect(DataPoint p)
        {
            float dist = distance(minCircle.C.Center, p);
            if (dist > treshold)
            {
                return true;
            }
            return false;
        }

        public void updateChooseAd(List<DataPoint> regPoints, List<DataPoint> runPoints, int time)
        {
            clearList();
            this.RegPoints = regPoints;
            this.RunPoints = runPoints;
            this.time = time;
            this.minCircle.C = minCircle.findMinCircle(regPoints, regPoints.Count());

            treshold = minCircle.C.Radius * 1.1;

            this.regPoints = regPoints;
            this.runPoints = runPoints;

            addPoints(0);

            oldTime = time;
        }

        //added to time one, added one point.
        public void nextTime()
        {
            DataPoint p = RunPoints[time];
            if (detect(p))
            {
                RedPoints.Add(p);
            }
            else
            {
                if (BluePoints.Count >= 30)
                {
                    DataPoint oldP = BluePoints[0];
                    BluePoints.RemoveAt(0);
                    GreyPoints.Add(oldP);
                }
                BluePoints.Add(p);
            }
        }

        //add points to lists from start until time
        public void addPoints(int start)
        {
            for (int i = start; i < time; i++)
            {
                DataPoint p = RunPoints[i];
                int diff = time - 30;

                if (detect(p))
                {
                    RedPoints.Add(p);
                }
                else
                {

                    if (i < diff)
                    {
                        GreyPoints.Add(p);
                    }
                    else
                    {
                        BluePoints.Add(p);
                    }
                }
            }
        }
        public void jumpForward()
        {
            addPoints(oldTime + 1);
        }

        public void clearList()
        {
            RedPoints.Clear();
            BluePoints.Clear();
            GreyPoints.Clear();
        }
        public void updateTimeAd(int time)
        {

            this.time = time;
            int diffTime = time - oldTime;
            if (diffTime == 0)
            {
                return;
            }
            if (diffTime == 1)
            {
                nextTime();
            }
            else if (diffTime > 1)
            {
                jumpForward();
            }
            else if (diffTime < 0)
            {
                addPoints(0);
            }
            oldTime = time;
        }
    }
}
