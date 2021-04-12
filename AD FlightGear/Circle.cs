using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace AD_FlightGear
{
	public class Circle
	{
		DataPoint center;
		float radius;
		public Circle(DataPoint p, float r)
		{
			this.center = p;
			this.radius = r;
		}

        public Circle() { }

        public DataPoint Center
		{
			get { return center; }
			set { center = value; }
		}

		public float Radius
		{
			get { return radius; }
			set { radius = value; }
		}
	}
}
