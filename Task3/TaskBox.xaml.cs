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
using System.Windows.Threading;


namespace Task3
{
    /// <summary>
    /// Interaction logic for TaskBox.xaml
    /// </summary>
    public partial class TaskBox : UserControl
    {
        static EventHandler startTimerHandler;
        static EventHandler pauseTimerHandler;
        static EventHandler stopTimerHandler;
        DispatcherTimer timer;
        Model model;
        Presenter p;
        bool timerIsActive;

        public int ID { get; set; }

        public TaskBox()
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this.timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            this.model = new Model();
        }

        /// <summary>
        /// Tick for a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            textBlock.Foreground = Brushes.Black;
            timer.Start();
            model.Time++;
            textBlock.Text = $"Logged: {model.ShowTime()}";
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
            else if (timerIsActive == true)
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
