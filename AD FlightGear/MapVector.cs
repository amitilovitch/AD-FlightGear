using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    public class MapVector
    {
        private string _name;
        public string _Name
        {
            get { return _name;}
            set {_name = value;}
        }
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _node;
        public string Node
        {
            get { return _node; }
            set { _node = value; }
        }

        public List<float> _vectorFloat
        {
            get; set;
        }
        public MapVector(string nameFeature, string type, string node)
        {
            _name = nameFeature;
            _type = type;
            _node = node;
            _vectorFloat = new List<float>();
        }
    }
}
