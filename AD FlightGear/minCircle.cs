using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace AD_FlightGear
{
    public class minCircle
    {
        Circle minC;

        public Circle MinC
        {
            get { return minC; }
            set { minC = value; }
        }
        //DataPoint center;
        //float radius;
        /*public minCircle(DataPoint p, float r)
        {
            base(p, r);
        }*/
        /**
         * returning the distance between 2 points given
         * @param p1 one of the points given
         * @param p2 second points given
         * @return the distance between the 2 points
         */
        public float distance(DataPoint p1, DataPoint p2)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
        }

        /**
         * creating a circle object from a vector containing 2 points
         * @param boundary the vector of points
         * @return circle from the 2 points
         */
        public Circle createCircleFrom2Points(DataPoint p1, DataPoint p2)
        {
            float x = Convert.ToSingle((p1.X + p2.X) / 2);
            float y = Convert.ToSingle((p1.Y + p2.Y) / 2);
            DataPoint center = new DataPoint(x, y);
            //DataPoint center(x, y);
            float radius = distance(p1, p2) / 2;
            Circle c = new Circle(center, radius);
            //Circle c(center, radius);
            return c;
        }

        /**
         * creating a circle with 3 points given. creating 2 lines of 2 intersecting points each.
         * then finding the vertical line for each line we found. then finding the intersecting points of those 2 lines
         * as the center of the circle. and finding the radius as the distance between the center of the circle
         * and the center of one of the lines we found at the beginning.
         * @param p1 first point given
         * @param p2 second point given
         * @param p3 third point given
         * @return circle of those 3 points
         */
        public Circle createCircleFrom3Points(DataPoint p1, DataPoint p2, DataPoint p3)
        {
            // line equation: y=mx+b
            DataPoint middle12 = new DataPoint((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2); // middle of line of points p1, p2
            DataPoint middle13 = new DataPoint((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2); // middle of line of points p1, p3

            // m = y1 - y2 / x1 - x2 - the slope of the line
            float m12 = Convert.ToSingle((p1.Y - p2.Y) / (p1.X - p2.X)); // the slope of line of points p1, p2
            float m13 = Convert.ToSingle((p1.Y - p3.Y) / (p1.X - p3.X)); // the slope of line of points p1, p3
            float m12vertical = -1 / m12; // the slope of the vertical line to m12
            float m13vertical = -1 / m13; // the slope of the vertical line to m13

            // b = y - mx - finding the b value to the vertical lines
            float b12vertical = Convert.ToSingle(middle12.Y - (m12vertical * middle12.X));
            float b13vertical = Convert.ToSingle(middle13.Y - (m13vertical * middle13.X));

            // finding the center of the circle
            float x = (b12vertical - b13vertical) / (m13vertical - m12vertical);
            float y = (m12vertical * x) + b12vertical;
            //Point center(x, y);
            DataPoint center = new DataPoint(x, y);

            // finding the radius of the circle
            float r = distance(center, p1);
            Circle c = new Circle(center, r);
            //Circle c(center, r);
            return c;
        }

        /**
         * this method creates a circle from a vector with up to 3 points.
         * if the vector is empty - it creates a default circle.
         * if the vector has one point - create a center of circle as the point and a default radius.
         * if the vector has 2 points - create the center as the middle point between the two ,
         * and the radius as half of the distance between the two points.
         * if the vector has 3 points - creating a circle normally with the 2 vertical lines to the lines created
         * by the 3 points.
         * @param boundary the vector of points on the boundary of the circle.
         * @return circle of those points
         */
        public Circle createCircle(List<DataPoint> boundary)
        {
            if (boundary.Count() == 0)
            {
                DataPoint p = new DataPoint(0, 0);
                //Point p(0,0);
                Circle c = new Circle(p, 0);
                //Circle c(p, 0);
                return c;
            }
            else if (boundary.Count() == 1)
            {
                
                Circle c = new Circle(boundary[0], 0);
                //Circle c(boundary[0], 0);
                return c;
            }
            else if (boundary.Count() == 2)
            {
                return createCircleFrom2Points(boundary[0], boundary[1]);
            }
            else
            { // boundary size = 3
              // first checking if we can create a circle with only 2 of the points -
              // creating a circle with each possible pair of points and then checking if the last point is in the circle
              // if so returning that circle.
                Circle c1 = createCircleFrom2Points(boundary[0], boundary[1]); // first possible circle
                if (distance(c1.Center, boundary[2]) <= c1.Radius)
                {
                    return c1;
                }
                c1 = createCircleFrom2Points(boundary[0], boundary[2]); // second possible circle
                if (distance(c1.Center, boundary[1]) <= c1.Radius)
                {
                    return c1;
                }
                c1 = createCircleFrom2Points(boundary[1], boundary[2]); // third possible circle
                if (distance(c1.Center, boundary[0]) <= c1.Radius)
                {
                    return c1;
                }
                // else creating a circle from the 3 points.
                return createCircleFrom3Points(boundary[0], boundary[1], boundary[2]);
            }
        }

        /**
         * a recursive method that "deletes" temporarily the last point in the vector each time ,and returning the
         * minimal circle of points without the point erased. later checing if the point erased is inside the circle -
         * if so - returning that circle, and if not - creating a new circle when the point erased must be on the boundary
         * of the new circle - and all of this is happening recursively.
         * @param points the vector of points that needs to be inside the circle
         * @param boundaryPoints the points that must be on the boundaries of the circle
         * @param size the size of the vector of points.
         * @return minimal circle around the points.
         */
        public Circle recursiveMinCircle(ref List<DataPoint> points, List<DataPoint> boundaryPoints, int size)
        {
            // if the vector of points is "empty" (empty from the part that we can look at),
            // or if the boundary has already 3 points we can create a circle.
            if (size == 0 || boundaryPoints.Count() == 3)
            {
                return createCircle(boundaryPoints);
            }
            // "deleting" temporarily 1 point from the vector and checking for the circle without that point
            Random r = new Random();
            int tmpErase = r.Next(size);
            //int tmpErase = rand() % size;
            DataPoint p = points[tmpErase];
            DataPoint temp = points[tmpErase];
            points[tmpErase] = points[size - 1];
            points[size - 1] = temp;
            //swap(points[tmpErase], points[size - 1]);
            Circle c = recursiveMinCircle(ref points, boundaryPoints, size - 1);

            // checking if the point "deleted" is inside the circle or on it's boundaries.
            if (distance(p, c.Center) > c.Radius)
            {
                // the point is outside the circle - must be in the boundary
                //boundaryPoints.push_back(p);
                boundaryPoints.Add(p);
            }
            else
            { // the point is inside.
                return c;
            }
            // checking again for a new circle since the point deleting
            // temporarily is not inside the circle we already have.
            return recursiveMinCircle(ref points, boundaryPoints, size - 1);
        }

        /**
         * our main function where we're creating a vector of points from the array of points given and
         * calling the recursive function that returns the minimal circle the contains all the points in the array.
         * @param points the array of points
         * @param size size of the array given
         * @return minimal circle that contains inside (or on it's boundaries) all the points in the array.
         */
        public minCircle(List<DataPoint> points, int size)
        {
            List<DataPoint> pointVec = new List<DataPoint>();
            for (int i = 0; i < size; i++)
            {
                pointVec.Add(points[i]);
                //pointVec.push_back(*points[i]);
            }
            List<DataPoint> boundaryPoints = new List<DataPoint>();
            Circle c = recursiveMinCircle(ref pointVec, boundaryPoints, pointVec.Count());
            this.minC = c;
            //return c; ///////////////
        }
    }
}
