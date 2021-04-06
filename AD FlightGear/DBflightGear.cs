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
    public class DBflightGear
    {
        private int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        private string[] _listLine;
        public string[] _ListLine
        {
            get { return _listLine;}
            set {_listLine = value;}
        }

        private string _pathCsv;
        public string _PathCsv
        {
            get {return _pathCsv;}
            set {_pathCsv = value;}
        }
        private string _pathXml;
        public string _PathXml
        {
            get { return _pathXml;}
            set {_pathXml = value;}
        }
        private List<string> _listFeature;
        public List<string> ListString
        {
            get { return _listFeature; }
        }
        //public List<string> _ListFeature = new List<string>();
        //public Dictionary<string, MapVector> _DictMapVector = new Dictionary<string, MapVector>();
        private List<MapVector> mapDb;
        public List<MapVector> MapDb
        {
            get { return mapDb; }
        }
        //public List<MapVector> mapDb = new List<MapVector>();
        public DBflightGear()
        {
            _listFeature = new List<string>();
             mapDb = new List<MapVector>();
        }

        public void createListLines()
        {
            _ListLine = File.ReadAllLines(_PathCsv);
            length = _listLine.Length;

            for (int i = 0; i < length; i++)
            {
                _ListLine[i] = _ListLine[i] + "\n";
            }
        }
        public void createListDataFeature()
        {
            XmlDocument reader = new XmlDocument();
            reader.Load(_PathXml);
            XmlNodeList NodeList = reader.GetElementsByTagName("node");
            XmlNodeList NameList = reader.GetElementsByTagName("name");
            XmlNodeList TypeList = reader.GetElementsByTagName("type");

            int size = NodeList.Count;

            for (int i = 0; i < size; i ++)
            {
                _listFeature.Add(NodeList[i].InnerText);
                if (NodeList[i].InnerText.Equals(_listFeature[0]) && (i > 0))
                {
                    _listFeature.RemoveAt(_listFeature.Count - 1);
                    break;
                }
                mapDb.Add(new MapVector(NameList[i].InnerText,
                    TypeList[i].InnerText,
                    NodeList[i].InnerText));
            }
        }
        public void createVectors()
        {

            int row = _ListLine.Length;
            for (int i = 0; i < row; i++)
            {
                string[] words = _ListLine[i].Split(',');
                int col = words.Length;

                for (int j = 0; j < col; j++)
                {
                    mapDb[j]._vectorFloat.Add(float.Parse(words[j]));
                    //_DictMapVector[_ListFeature[j]]._vectorFloat.Add(float.Parse(words[j]));
                }
            }
        }


        public void InitializeDB()
        {
            createListLines();
            createListDataFeature();
            createVectors();
        }
    }
}



