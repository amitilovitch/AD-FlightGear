using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{

	class MinCircle
	{

		private Circle c;

		public Circle C { get; set; }

		public MinCircle()
		{
			this.c = new Circle();
		}
		// calculate the distance between two points
		public float my_distance(DataPoint p1, DataPoint p2)
		{
			double x1 = p1.X;
			double y1 = p1.Y;
			double x2 = p2.X;
			double y2 = p2.Y;
			return (float)(Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
		}
		static void swap(int indexA, int indexB, ref List<DataPoint> points)
		{
			DataPoint temp = points[indexA];
			points[indexA] = points[indexB];
			points[indexB] = temp;
		}
		// return the circle with one point or two points or three points
		public Circle basicCircle(List<DataPoint> points_circle)
		{
			// if we dont have border points
			if (points_circle.Count() == 0)
			{
				//there is no points border
				return new Circle(new DataPoint(0, 0), 0);
			}
			//if we have one border point
			else if (points_circle.Count() == 1)
			{
				return new Circle(points_circle[0], 0);
			}
			//if we have two border points - calculate the circle
			else if (points_circle.Count() == 2)
			{
				float radius = (my_distance(points_circle[0], points_circle[1])) / 2;
				double x_circle = (points_circle[0].X + points_circle[1].X) / 2;
				double y_circle = (points_circle[0].Y + points_circle[1].Y) / 2;
				return new Circle(new DataPoint(x_circle, y_circle), radius);
			}
			else
			{ // if we have three border points
			  //we found equation in the web to calculate the center of the circle when we have 3 points:
				double x1 = points_circle[0].X;
				double y1 = points_circle[0].Y;
				double x2 = points_circle[1].X;
				double y2 = points_circle[1].Y;
				double x3 = points_circle[2].X;
				double y3 = points_circle[2].Y;

				double A = x1 * (y2 - y3) - y1 * (x2 - x3) + x2 * y3 - x3 * y2;
				double B = (x1 * x1 + y1 * y1) * (y3 - y2) + (x2 * x2 + y2 * y2) * (y1 - y3) + (x3 * x3 + y3 * y3) * (y2 - y1);
				double C = (x1 * x1 + y1 * y1) * (x2 - x3) + (x2 * x2 + y2 * y2) * (x3 - x1) + (x3 * x3 + y3 * y3) * (x1 - x2);
				double D = (x1 * x1 + y1 * y1) * (x3 * y2 - x2 * y3) + (x2 * x2 + y2 * y2) * (x1 * y3 - x3 * y1) + (x3 * x3 + y3 * y3) * (x2 * y1 - x1 * y2);

				double x_center = -1 * B / (2 * A);
				double y_center = -1 * C / (2 * A);
				double radius = Math.Sqrt(Math.Pow(x_center - x1, 2) + Math.Pow(y_center - y1, 2));
				return new Circle(new DataPoint((float)x_center, (float)y_center), (float)radius);
			}
		}

		// checking if the last_point inside the circle
		public bool pointIsInCircle(DataPoint last_point, Circle rec_circle)
		{
			float dis = my_distance(last_point, rec_circle.Center);
			if (dis > rec_circle.Radius)
			{
				return false;
			}
			//else : distance <= radius
			return true;
		}

		// the function caluculate recursivly the minimum circle
		public Circle min_circle_helper(List<DataPoint> points, List<DataPoint> points_circle, int size)
		{
			if (points_circle.Count > 3)
            {
				return C;
            }
			//base recusion
			if (size == 0 || points_circle.Count() == 3)
			{
				c = basicCircle(points_circle);
				return c;
			}

			DataPoint p = points[size-1];

			Circle rec_circle = min_circle_helper(points, points_circle, size - 1);
			//if the last point is inside the circle - we dont need to change the borders.
			if (pointIsInCircle(p, rec_circle))
			{
				c = rec_circle;
				return c;
			}
			//if the last point is not inside the circle - the point should be border point
			points_circle.Add(p);
			//return in recrsion the circle with the new point border
			return min_circle_helper(points, points_circle, size - 1);
		}
	

		// the function call to recursive method that return the minimum circle
		public Circle findMinCircle(List<DataPoint> points, int size)
		{
			List<DataPoint> points_circle = new List<DataPoint>();
			 this.c = min_circle_helper(points, points_circle, size);
			return c;
		}
	}
}
