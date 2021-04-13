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
using Emoji;/// /////////////////////////////////////////////


namespace AD_FlightGear.Controls
{
    /// <summary>
    /// Interaction logic for playPauseButton.xaml
    /// </summary>
    public partial class playStopButtons : UserControl
    {
        private playStopButtonsVM vm;
        private bool pauseFlag;
        private bool stopFlag;
        private bool firstP;

        public playStopButtons()
        {
            InitializeComponent();
            pauseFlag = true;
            stopFlag = true;
            firstP = true;
        }

        public void setVM(playStopButtonsVM vm)
        {
            this.vm = vm;
        }

        private void playButtonClick(object sender, RoutedEventArgs e)
        {
            if (vm.IsRunLoaded&&vm.IsRegLoaded){
                if (firstP == true && pauseFlag == true && stopFlag == true)
                {
                    // play after pressed stop - starting loop from the start.
                    firstP = false;
                    pauseFlag = false;
                    stopFlag = false;
                    vm.StartVideo(false, false);
                    this.playButton.Content = "⏸";
                }
                else if (firstP == false && pauseFlag == false && stopFlag == false)
                {
                    // pressed paused after loop started - needs to stop loop for the time being
                    pauseFlag = true;
                    vm.SetPause(true);
                    this.playButton.Content = "▶️";
                }
                else if (firstP == false && pauseFlag == true && stopFlag == false)
                {
                    // pressed play after the loop started - continue the loop
                    pauseFlag = false;
                    vm.SetPause(false);
                    this.playButton.Content = "⏸";

                }
            }
        }
        private void stopButtonClick(object sender, RoutedEventArgs e)
        {
            if (stopFlag == false)
            {
                stopFlag = true;
                pauseFlag = true;
                firstP = true;
                vm.SetStop(true);
                this.playButton.Content = "▶️";
            }
        }
    }
}
