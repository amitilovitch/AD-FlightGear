/*using OxyPlot;
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
            this.greyPoints = new List<DataPoint>();
            this.bluePoints = new List<DataPoint>();
            this.redPoints = new List<DataPoint>();

            this.listPointCircle = new List<DataPoint>();
            this.minCircle = new MinCircle();

            this.regPoints = new List<DataPoint>();
            this.runPoints = new List<DataPoint>();
            oldTime = 0;
        }

        public void initMinCircle()
        {
            radius = this.minCircle.C.Radius;
            listPointCircle[0] = minCircle.C.Center;
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
            this.regPoints = regPoints;
            this.runPoints = runPoints;
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
            DataPoint p = runPoints[time];
            if (detect(p))
            {
                redPoints.Add(p);
            } else
            {
                bluePoints.Add(p);
            }

            if (bluePoints.Count >= 30)
            {
                DataPoint oldP = bluePoints[0];
                bluePoints.RemoveAt(0);
                greyPoints.Add(oldP);
            }
        }

        //add points to lists from start until time
        public void addPoints(int start)
        {
            for (int i = start; i < time; i++)
            {
                DataPoint p = runPoints[i];
                int diff = time - 30;

                if (detect(p))
                {
                    redPoints.Add(p);
                }
                else
                {

                    if (i < diff)
                    {
                        greyPoints.Add(p);
                    }
                    else
                    {
                        bluePoints.Add(p);
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
            redPoints.Clear();
            bluePoints.Clear();
            greyPoints.Clear();
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
                clearList();
                addPoints(0);
            }
            oldTime = time;
        }

        public float f(float x)
        {
            // returns y value on line of the x given
            return a * x + b;
        }
    }

}*/