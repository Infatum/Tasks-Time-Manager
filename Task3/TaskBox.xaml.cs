using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Task3
{
    /// <summary>
    /// Interaction logic for TaskBox.xaml
    /// </summary>
    public partial class TaskBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        DispatcherTimer timer;
        Model model;
        Presenter p;
        bool timerIsActive;
        string _title;

        public TaskBox()
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this.model = new Model();
            this.DataContext = this.model;
        }

        public int ID { get; set; }

        /// <summary>
        /// Tick for a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            model.Timer_Tick(sender, e);
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Stop(object sender, EventArgs e)
        {
            //mainwindow.textBlockCountDown.Text = "";
            timer.IsEnabled = false;
            model.ResetTime();
        }

        /// <summary>
        /// Start/pause timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartPause_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (timerIsActive == false)
            {
                model.StartResumeTimer();
                b.Content = "Pause";
                b.Background = Brushes.DarkGray;
                timerIsActive = true;
            }
            else
            {
                model.StopPauseTimer();
                textBlock.Foreground = Brushes.Blue;
                b.Content = "Куігьу";
                b.Background = Brushes.Gray;
                timerIsActive = false;
            }
        }
    }
}
