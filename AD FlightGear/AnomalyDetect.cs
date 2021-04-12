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




      private minCircle minCircle;


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
      public AnomalyDetect() ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      {
          this.GreyPoints = new List<DataPoint>();
          this.BluePoints = new List<DataPoint>();
          this.RedPoints = new List<DataPoint>();

          this.listPointCircle = new List<DataPoint>();

          this.RegPoints = new List<DataPoint>();
          this.RunPoints = new List<DataPoint>();
      }

      public void initMinCircle()
      {
          radius = this.minCircle.MinC.Radius;
          ListPointCircle[0] = minCircle.MinC.Center;
      }


      public float distance(DataPoint p1, DataPoint p2)
      {
          return Convert.ToSingle(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
      }
      //true is detect anomaly
      public bool detect(DataPoint p)
      {
          float dist = distance(minCircle.MinC.Center, p);
          if (dist > treshold)
          {
              return true;
          }
          return false;
      }

      public void updateChooseAd(List<DataPoint> regPoints, List<DataPoint> runPoints, int time)
      {
          this.minCircle = new minCircle(regPoints, regPoints.Count());

          treshold = minCircle.MinC.Radius * 1.1;

          this.regPoints = regPoints;
          this.runPoints = runPoints;

          OldTime = Time;
          addPoints(0);
      }

      //added to time one, added one point.
      public void nextTime()
      {
          DataPoint p = RunPoints[Time];
          if (detect(p))
          {
              RedPoints.Add(p);
          }

          DataPoint oldP = BluePoints[0];
          BluePoints.Add(p);
          if (Time > 30)
          {
              BluePoints.RemoveAt(0);
              GreyPoints.Add(oldP);
          }
      }

      //add points to lists from start until time
      public void addPoints(int start)
      {
          for (int i = start; i < Time; i++)
          {
              DataPoint p = RunPoints[i];
              int diff = Time - 30;

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
          addPoints(OldTime + 1);
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
          int diffTime = Time - OldTime;
          if (diffTime == 1)
          {
              nextTime();
          }
          else if (diffTime > 1)
          {
              jumpForward();
          }
          else if (diffTime < 1)
          {
              addPoints(0);
          }

  }
}
}*/

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

        public List<DataPoint> listPointLine;
        public List<DataPoint> ListPointLine { get; set; }

        //lists point from app

        private List<DataPoint> regPoints;
        public List<DataPoint> RegPoints { get; set; }
        private List<DataPoint> runPoints;

        public List<DataPoint> RunPoints { get; set; }


        //line fields
        private Line regressLine;
        public Line RegressLine { get; set; }

        private int time;
        public int Time { get; set; }


        private int oldTime;
        public int OldTime { get; set; }
        private double treshold;
        public double Treshold { get; set; }

        private float x_max;
        public float X_max { get; set; }





        public AnomalyDetect()
        {
            GreyPoints = new List<DataPoint>();
            BluePoints = new List<DataPoint>();
            redPoints = new List<DataPoint>();

            ListPointLine = new List<DataPoint>();

            this.regPoints = new List<DataPoint>();
            this.runPoints = new List<DataPoint>();

        }


        float avg(float[] x, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        float var(float[] x, int size)
        {
            float av = avg(x, size);
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        float cov(float[] x, float[] y, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - avg(x, size) * avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        float pearson(float[] x, float[] y, int size)
        {
            if (size == 0)
            {
                return 0;
            }
            float tempCov = cov(x, y, size);
            double sqrtt = System.Math.Sqrt(var(x, size)) * System.Math.Sqrt(var(y, size));
            if (tempCov == 0 || sqrtt == 0)
            {
                return 0;
            }

            float sqrt = (float)sqrtt;
            return tempCov / sqrt;
        }

        public Line linear_reg(ref List<DataPoint> points, int size)
        {
            X_max = 0;
            float[] x = new float[size];
            float[] y = new float[size];
            // creating 2 arrays: one for the x values and one for the y values of the point.
            for (int i = 0; i < size; i++)
            {
                x[i] = Convert.ToSingle(points[i].X);
                y[i] = Convert.ToSingle(points[i].Y);

                if (x[i] > X_max)
                {
                    X_max = x[i];
                }
            }
            float a = cov(x, y, size) / var(x, size);
            float b = avg(y, size) - (a * avg(x, size));

            return new Line(a, b);
        }

        public void initListPointLine()
        {
            ListPointLine.Clear();
            ListPointLine.Add(new DataPoint(0, Convert.ToDouble(regressLine.f(0))));
            ListPointLine.Add(new DataPoint(Convert.ToDouble(x_max), Convert.ToDouble(regressLine.f(x_max))));
        }

        // returns the deviation between point p and the line equation of the points array given.
        public float dev(DataPoint p, List<DataPoint> points, int size)
        {
            Line line = new Line();
            line = linear_reg(ref points, size);
            return dev(p, line);
        }

        // returns the deviation between point p and the line.
        public float dev(DataPoint p, Line l)
        {
            return (float)Math.Abs(l.f((float)p.X) - p.Y);
        }

        public void learnTresh()
        {
            float max = 0;
            float val_tmp = 0;
            this.regressLine = linear_reg(ref regPoints, regPoints.Count);
            for (int i = 0; i < regPoints.Count; i++)
            {
                val_tmp = dev(regPoints[i], regressLine);
                if (val_tmp > max)
                {
                    max = val_tmp;
                }
            }
            treshold = (float)(max * 1.1);
        }
        //true is detect anomaly
        public bool detect(DataPoint p)
        {
            float val = dev(p, regressLine);
            if (val > treshold)
            {
                return true;
            }
            return false;
        }

        public void updateChooseAd(List<DataPoint> regPoints, List<DataPoint> runPoints, int time)
        {
            this.RegPoints = regPoints;
            this.RunPoints = runPoints;
            this.Time = time;
            learnTresh(); // למידה 
            OldTime = Time; ///  שמירה של הזמן הקודם
            addPoints(0);
        }

        //added to time one, added one point.
        public void nextTime()
        {
            DataPoint p = RunPoints[Time];
            if (detect(p))
            {
                RedPoints.Add(p);
            }
            else { BluePoints.Add(p); }
            if ((BluePoints.Count != 0 )&&(Time > 30))
            {
                DataPoint oldP = BluePoints[0];
                BluePoints.RemoveAt(0);
                GreyPoints.Add(oldP);
            }


        }

        //add points to lists from start until time
        public void addPoints(int start) // מקבל 
        {
            for (int i = start; i < Time; i++)
            {
                DataPoint p = RunPoints[i];
                int diff = Time - 30;

                if (detect(p))
                {
                    RedPoints.Add(p);// חריג 
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
            addPoints(OldTime + 1);
        }

        public void clearList()
        {
            RedPoints.Clear();
            BluePoints.Clear();
            GreyPoints.Clear();
        }
        public void updateTimeAd(int time)
        {
            Time = time;// this.time = time;
            int diffTime = Time - OldTime;
            if (diffTime == 1)  // הוספת נקודה אחת 
            {
                nextTime();
            }
            else if (diffTime > 1)// הזזה קדימה
            {
                jumpForward(); // מוסיפים עוד נקודות
            }
            else if (diffTime < 1) // קפיצה אחורה
            {
                addPoints(0); // מנקים את הרשימות 
            }
        }
    }




    public class Line
    {
        float a, b;
        public Line()
        {
            this.a = 0;
            this.b = 0;
        }
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public float f(float x)
        {
            // returns y value on line of the x given
            return a * x + b;
        }
    }

}