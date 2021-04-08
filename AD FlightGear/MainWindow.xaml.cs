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
            //modelFG.Initialize();
            VM_FlightData vM_FlightData = new VM_FlightData(modelFG);
            FlightData.setVm_flightData(vM_FlightData);

            twoSlidersVM vm_twoSliders = new twoSlidersVM(modelFG);
            this.sliders2.setVM(vm_twoSliders);

            joystickVM vm_Joystick = new joystickVM(modelFG);
            this.joystick.setVM(vm_Joystick);

            graphs_vm graphs = new graphs_vm(modelFG);
            dataGraphV.set_graphs_VM(graphs);
            dataGraphV.addButtons();
            VM_OpenFiles vM_OpenFiles = new VM_OpenFiles(modelFG);
            OpenFilesV.setVM_OpenFiles(vM_OpenFiles);
/*            Assembly dll = Assembly.LoadFile(@"C:\Users\azran\source\repos\dllAnomally\dllAnomally\bin\Debug\dllAnomally.dll");
            Type[] type = dll.GetExportedTypes();
            c = Activator.CreateInstance(type[0]);
            ohad.Children.Add(c);*/
        } 

        private void FlightData_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
