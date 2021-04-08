using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace AD_FlightGear.Controls
{
    /// <summary>
    /// Interaction logic for OpenFilesV.xaml
    /// </summary>
    public partial class OpenFilesV : System.Windows.Controls.UserControl
    {
        private VM_OpenFiles vM_OpenFiles;

        public void setVM_OpenFiles(VM_OpenFiles vM_Open)
        {
            this.vM_OpenFiles = vM_Open;
            DataContext = vM_OpenFiles;
        }
        public OpenFilesV()
        {
            InitializeComponent();
        }


        private void Button_csv_reg(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                vM_OpenFiles.VM_PathCsv = openFileDialog.FileNames[0];
            }
            vM_OpenFiles.initDBreg();
        }

        private void Button_dll(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                vM_OpenFiles.VM_PathDll = openFileDialog.FileNames[0];
            }
        }

        private void openCsvRun(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                vM_OpenFiles.VM_PathCsvReg = openFileDialog.FileNames[0];
            }
            vM_OpenFiles.initDBrun();
        }
    }
}
