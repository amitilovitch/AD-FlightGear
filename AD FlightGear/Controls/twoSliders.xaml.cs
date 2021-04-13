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

namespace AD_FlightGear.Controls
{
    /// <summary>
    /// Interaction logic for twoSliders.xaml
    /// </summary>
    public partial class twoSliders : UserControl
    {
        public twoSliders()
        {
            InitializeComponent();

        }

        private twoSlidersVM vm;
        public twoSlidersVM VM
        {
            get { return vm; }
            set { this.vm = value; }
        }

        public void setVM(twoSlidersVM newvm) 
        {
            this.vm = newvm;
            DataContext = vm;
        }

        private void rudderSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
