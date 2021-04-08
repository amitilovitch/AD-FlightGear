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

        private int hdgIndex;
        public int HdgIndex { get; set; }

        private int altIndex;
        public int AltIndex { get; set; }

        private int speedIndex;
        public int SpeedIndex { get; set; }

        private int pitchIndex;
        public int PitchIndex { get; set; }

        private int rollIndex;
        public int RollIndex { get; set; }

        private int yawIndex;
        public int YawIndex { get; set; }

        private int throttle0Index;
        public int Throttle0Index { get; set; }

        private int throttle1Index;
        public int Throttle1Index { get; set; }

        private int rudderIndex;
        public int RudderIndex { get; set; }

        private int alieronIndex;
        public int AlieronIndex { get; set; }

        private int elevatorIndex;
        public int ElevatorIndex { get; set; }

        private List<string> _listFeature;
        public List<string> ListString
        {
            get { return _listFeature; }
        }

        private List<MapVector> mapDb;
        public List<MapVector> MapDb
        {
            get { return mapDb; }
        }
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
                    NodeList[i].InnerText, i));
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
                }
            }
        }

        public void findIndexFeatures()
        {

            for (int i = 0; i < _listFeature.Count; i++ )
            {
                string s = mapDb[i].Node;
                switch (s)
                {
                    case "/instrumentation/heading-indicator/indicated-heading-deg":
                        HdgIndex = i;
                        break;
                    case "/instrumentation/altimeter/indicated-altitude-ft":
                        AltIndex = i;
                        break;
                    case "/instrumentation/airspeed-indicator/indicated-speed-kt":
                        SpeedIndex = i;
                        break;
                    case "/orientation/pitch-deg":
                        PitchIndex = i;
                        break;
                    case "/orientation/roll-deg":
                        RollIndex = i;
                        break;
                    case "/orientation/side-slip-deg":
                        YawIndex = i;
                        break;
                    case "rudder":
                        RudderIndex = i;
                        break;
                    case "/controls/engines/engine[1]/throttle":
                        Throttle1Index = i;
                        break;
                    case "/controls/engines/engine[0]/throttle":
                        Throttle0Index = i;
                        break;
                    case "/controls/flight/aileron[0]":
                        AlieronIndex = i;
                        break;
                    case "/controls/flight/elevator":
                        ElevatorIndex = i;
                        break;
                    default:
                        break;
                }

            }
        } 


        public void InitializeDB()
        {
            createListLines();
            createListDataFeature();
            createVectors();
            findIndexFeatures();
        }
    }
}



