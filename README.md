

# **welcome to the interactive AD-Flight-Gear experience!**

VERSION:
1.0.0

## *Description:*
This app can give a way for flight investigators (such as pilots and for military use) to ivestigate the anomalies of a specific 
flight, by given the app 2 files - one with details of a trained flight (with no anomalies), and one with the suspected anomalied 
flight. At the same time you can also see a flight simulation in another screen of the app.
Features in the app:
You can play the flight from the beginning and pause at each second you want. You can also skip to a different part of the flight
(by sliding our time slider). You can speed parts of the flight that are with no change to any speed you'd like.
You can look at the timer to see how much time had passed, and how much time is left.
You can see in the chart flight data such as: altimeter, airspeed, pitch, roll and more, and see how they change through time.
You can see the changes of the throttle and rudder at the sliders, and see the changes of aileron and elevator in the joystick 
feature on the app.
You can upload a dll file of a new anomaly detection algorithm or use one of our given algorithms to check and present the 
anomalies.
This app is great for military use, for flight investigators and for pilots looking to check the anomalies in their flight.



## *Demos:*
A link to the video of a demonstration:
[demo](https://drive.google.com/drive/folders/1nEP4WTSDDoQ6AYTlj1NRHfE4ubdrfn3Y?usp=sharing)

A link to the UML of the project:
[UML](https://github.com/azranohad/AD-FlightGear/blob/e77eb1d23a48e8548286baaa257acfc46f1c0859/AD%20FlightGear/ClassDiagram2.cd)

(in order to watch the UML in full you need to download the CLASS DESIGNER plugin to visual studio). 

A link to the UML in picture format (is also inside the repository)
[picture UML](https://github.com/azranohad/AD-FlightGear/blob/180aa087da888585de0063c0708cf5aef3cce53d/UML%20picture.PNG)


## *Technologies we used:*
Oxyplot - an open source plot generation library that used to create charts and more.

Flight-Gear - usage of filght simulator 

Main code was written in C#, with target framework of .NET 4.7.2.


## *Installation:*
Required:
1. Install OxyPlot on your workspace (i.e visual studio 2019). right click on the project name -> manage NuGet packages -> 
   writing oxplot and downloading it. 
   
2. Download Flight-Gear flight simulator from https://www.flightgear.org/ 
3. Put the playback_small.xml file in FlightGear\data\Protocol folder.
4. Go to Flight-Gear settings, and insert the following line in additional settings :
 	```
	--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
    --fdm=null
	```
5. Click "fly" and wait until it's done loading.

6. If Oxyplot is already downloaded, there could be a problem that the program won't recognize it. So you sould uninstall and reinstall the Oxyplot for the app to work.
   
7. Launch the app,upload the playback_small.xml file, and upload 2 csv files of the flight data (the files should be different for the anomalies to be detected). 
   
   It is optional to choose the anomaly detection algorithm (from what's given or by uploading yourself), and if not picked 
   there will be a default algorithm chosen. 
   
   **If one of the dll files are not loaded, follow the instructions below:**
     - Right-click on your file.
     - Select Properties.
     - Make sure that you are on General tab.
     - In the Security option, you may see the message This file came from another computer and might be blocked to help protect this computer. Click on Unblock button.


## *How to contribute:*
You can add a dll file with the anomaly detection algorithm. This algorithm need to implement the interface below:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using OxyPlot;

namespace AD_FlightGear
{
    public interface GraphInterface
    {

        public AnomalyDetect Ad { get; set; }
        public Graph_view Uc { get; set; }

        public Graph_view create(){ }

        public void updateChoose(List<DataPoint> regPoints, List<DataPoint> runPoints, int time){ }
        public void updateTime(int time){ }
    }
}
```
Where the create() method creates Graph_view which is the view object of the dll.
The updateChoose() method updates the view if there was a change in the data chosen to present on the graph.
The updateTime() method updates the view when the time was changed and views it accordingly. 
There will also be an implementation of 2 properties: one of the user control (Graph_view), and one of the model (AnomalyDetect).
More information on the dll:
The dll will consists of a wpf user control which will be the view and an algorithm that will be implementing the 
interface given above. The view needs to be according to the algorithm you're giving. 

## *Contributors:*
Amit Ilovitch, Hila Levi, Ohad Azran, Lior Levi

![Image of Yaktocat](https://github.com/azranohad/AD-FlightGear/blob/master/AD%20FlightGear/screen%20shot%202.PNG)
![Image of Yaktocat](https://github.com/azranohad/AD-FlightGear/blob/master/AD%20FlightGear/screen%20shot%203.PNG)
![Image of Yaktocat](https://github.com/azranohad/AD-FlightGear/blob/master/AD%20FlightGear/data%20screen%201.PNG)
![Image of Yaktocat](https://github.com/azranohad/AD-FlightGear/blob/master/AD%20FlightGear/data%20screen%202.PNG)
