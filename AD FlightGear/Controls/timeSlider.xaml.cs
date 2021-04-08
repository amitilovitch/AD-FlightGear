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
    /// Interaction logic for timeSlider.xaml
    /// </summary>
    public partial class timeSlider : UserControl
    {
        private timeSliderViewModel vm;

        public timeSlider()
        {
            InitializeComponent();
        }

        public void setVM(timeSliderViewModel vm)
        {
            this.vm = vm;
            DataContext = vm;
        }

        private void changeValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // calling a function that will call the model and change the time property
            // and then the start loop will continue from a different location.
            vm.changeTime(e.NewValue);
            vm.changeClockTime();
        }
    }
}
