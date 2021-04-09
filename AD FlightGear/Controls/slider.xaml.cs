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
    /// Interaction logic for slider.xaml
    /// </summary>
    public partial class slider : UserControl
    {
        timeSliderViewModel vm;
        public slider()
        {
            InitializeComponent();
        }

        private void f(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.changeTime(e.NewValue);
            vm.changeClockTime();
        }

        public void setVM(timeSliderViewModel vm)
        {
            this.vm = vm;
            DataContext = vm;
        }
    }
}
