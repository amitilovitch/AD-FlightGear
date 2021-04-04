using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AD_FlightGear
{
    class DBflightGear
    {
        public string[] _ListLine
        {
            get; set;
        }
        public string _PathCsv
        {
            get; set;
        }
        public string _PathXml
        {
            get; set;
        }
        public List<string> _ListFeature = new List<string>();
        public Dictionary<string, List<float>> DictFeature = new Dictionary<string, List<float>>();


        public void createListLines()
        {
            _ListLine = File.ReadAllLines(_PathCsv);
        }
        public void createListDataFeature()
        {
            XmlDocument reader = new XmlDocument();
            reader.Load(_PathXml);
            XmlNodeList NodeList = reader.GetElementsByTagName("node");

            int size = NodeList.Count;

            for (int i = 0; i < size; i ++)
            {
                _ListFeature.Add(NodeList[i].InnerText);
                if (NodeList[i].InnerText.Equals(_ListFeature[0]) && (i > 0))
                {
                    _ListFeature.RemoveAt(_ListFeature.Count - 1);
                    break;
                }

            }
        }

        public void createDictFeature()
        {
            createListLines();
            createListDataFeature();
            int col = _ListFeature.Count;
            int rows = _ListLine.Length;

            for (int i = 0; i < col; i++)
            {
                DictFeature.Add(_ListFeature[i], new List<float>());
                string[] tokens = _ListLine[i].Split(',');
                for (int j = 0; j < rows; j++)
                {
                    DictFeature[_ListFeature[i]].Add(float.Parse(tokens[i]));
                }
            }
        }
    }
}



