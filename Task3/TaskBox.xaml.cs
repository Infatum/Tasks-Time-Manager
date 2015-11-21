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



        public int ID { get; set; }
        public int Timer
        {
            get { return this.model.Time; }
            set
            {
                this.model.Time = value;
                this.NotifyPropertyChanged("TaskTimerText");
            }
        }

        public string TaskTimerText
        { 
            get { return model.ShowTime(); }
        }

        public string TaskTitleText
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this.NotifyPropertyChanged("TaskTitleText");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public TaskBox()
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this.timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            this.model = new Model();
            this.DataContext = this;
        }

        /// <summary>
        /// Tick for a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            textBlock.Foreground = Brushes.Black;
            //model.Time++;
            this.Timer++;
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
                timer.Tick += Timer_Tick;
                timer.Start();
                b.Content = "Pause";
                b.Background = Brushes.DarkGray;
                timerIsActive = true;
            }
            else
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
                textBlock.Foreground = Brushes.Blue;
                b.Content = "Start";
                b.Background = Brushes.Gray;
                timerIsActive = false;
            }
        }
    }
}
