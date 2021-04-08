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
    /// Interaction logic for joystick.xaml
    /// </summary>
    public partial class joystick : UserControl
    {
        public joystick()
        {
            InitializeComponent();
            //this.stick.Canvas
        }
        private joystickVM vm;
        public joystickVM VM
        {
            get { return vm; }
            set { this.vm = value; }
        }
        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }
        public void setVM(joystickVM newvm) /// לבדוק אם הוא יכול גם ללכת לסטר של התכונה
        {
            this.vm = newvm;
            DataContext = vm;
        }
    }
}
