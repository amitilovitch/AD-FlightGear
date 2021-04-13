using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace AD_FlightGear
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dynamic c;

        public MainWindow()
        {
            
            InitializeComponent();
            ModelFG modelFG = new ModelFG();

            VM_FlightData vM_FlightData = new VM_FlightData(modelFG);
            FlightData.setVm_flightData(vM_FlightData);

            twoSlidersVM vm_twoSliders = new twoSlidersVM(modelFG);
            this.sliders2.setVM(vm_twoSliders);

            speedVM vm_speed = new speedVM(modelFG);
            this.speed.setVM(vm_speed);

            joystickVM vm_joystick = new joystickVM(modelFG);
            this.joystick.setVM(vm_joystick);

            timeSliderViewModel vm_timeSlider = new timeSliderViewModel(modelFG);
            this.timeSliders.setVM(vm_timeSlider);
            modelFG.Time = 0;
            modelFG.Length = 100;
            modelFG.Correlation = "Correaltion - ";

            playStopButtonsVM vm_buttons = new playStopButtonsVM(modelFG);
            this.playStopButtons.setVM(vm_buttons);

            ClockVM vm_clock = new ClockVM(modelFG);
            this.clock.setVM(vm_clock);

            graphs_vm graphs = new graphs_vm(modelFG);
            dataGraph_V.set_graphs_VM(graphs);


        } 

        private void FlightData_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void sliders2_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dataGraphV_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void clock_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void FlightData_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void joystick_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void timeSliders_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void speed_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
