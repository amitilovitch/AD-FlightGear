using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_FlightGear
{
    class MapVector
    {

        string _feature
        {
            get; set;
        }
        public List<float> _vectorFloat
        {
            get; set;
        }
        public MapVector(string nameFeature)
        {

            _feature = nameFeature;
            _vectorFloat = new List<float>();

            /* int size = _ListLine.Length;
             for (int i = 0; i < size; i++)
             {
                 //string ds = _ListLine[i];
                 _vectorFloat = new List<float>();
                 string[] words = _ListLine[i].Split(',');
                 for (int j = 0; j < words.Length; j++)
                 {
                     _vectorFloat.Add(float.Parse(words[j]));
                 }
             }*/
        }
    }
}
