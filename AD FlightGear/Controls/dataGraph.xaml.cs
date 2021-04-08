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

namespace AD_FlightGear.Controls
{
    using System.Collections.Generic;
    using System.Windows;
    using OxyPlot;
    /// <summary>
    /// Interaction logic for dataGraph.xaml
    /// </summary>
    public partial class dataGraph : UserControl
    {
        private int length;
        public string Title_Left { get; set; }
        public Button selectedItem { get; set; }
        
        public IList<Button> buttons { get; set; }
        
        private graphs_vm graphs_VM;
        private dynamic c;

        public void set_graphs_VM(graphs_vm graphs_)
        {
            
                this.graphs_VM = graphs_;
                DataContext = graphs_VM;
            
        }
        public dataGraph()

        {
            InitializeComponent();
            buttons = new List<Button>();
        }
        
/*          public void initializeDll()
         {
              try{
              Assembly dll = Assembly.LoadFile(PathDll);
                Type[] type = dll.GetExportedTypes();

                foreach(Type t in type)
                {
                    if (t.Name == "Graph_I")
                    {
                        c = Activator.CreateInstance(t);
                    }
                }
                DLLgraph.Children.Add(c.create());
            }
            catch { "error dll"; }*/
         // }
/*
         * 
         * public void updateChoose()
         * {
         *     c.updateChoose(PointsRun, PointsReg, Time);
         * 
         * }

         * public void updateTime(double time)
         * {
         * 
         *    // update all the graphs
         *    c.updateTime(Time);
         * }
         */
public void addButtons()
        {
            length = graphs_VM.VM_dBflight.MapDb.Count;
            for (int i = 0; i < graphs_VM.VM_dBflight.MapDb.Count; i++)
            {
                buttons.Add(new Button { ButtonContent = graphs_VM.VM_dBflight.MapDb[i].Name, ButtonID = (i).ToString() });
                
            }
            data_list.ItemsSource = buttons;



        }
        public class Button
        {
            public string ButtonContent { get; set; }
            public string ButtonID { get; set; }
        }

        public void data_list_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
        {
            if (data_list.SelectedItem != null)
            {
                object selectedItem_object = data_list.SelectedItem;
                selectedItem = (Button)selectedItem_object;

                graphs_VM.DataPoints_6(int.Parse(selectedItem.ButtonID));
                //InitializeComponent();
                //DataContext = graphs_VM;
            }
        }
    

        private void ic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void data_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
