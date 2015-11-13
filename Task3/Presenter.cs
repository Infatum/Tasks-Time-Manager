using System;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace Task3
{
    public class Presenter
    {
        Model model = null;
        MainWindow mainwindow = null;
        DispatcherTimer Timer;
        int n = 0;

        public Presenter(MainWindow mainWindow)
        {
            this.model = new Model();
            this.mainwindow = mainWindow;
            this.Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            this.Timer.Tick += Timer_Tick;
            this.mainwindow.StartTimer += Timer_Tick;
            this.mainwindow.PauseTimer += Timer_Pause;
            this.mainwindow.StopTimer += Timer_Stop;

        }
        internal Model Model
        {

            get { return model; }
            set { model = value; }
        }
        /// <summary>
        /// Stops the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Stop(object sender, EventArgs e)
        {
            //mainwindow.textBlockCountDown.Text = "";
            Timer.IsEnabled = false;
            model.ResetTime();
        }
        /// <summary>
        /// Pauses the Timer and colors numbers into Red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Pause(object sender, EventArgs e)
        {
            Timer.Stop();
            this.mainwindow.tb.textBlock.Foreground = Brushes.Blue;
        }
        /// <summary>
        /// Tick for a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            mainwindow.tb.textBlock.Foreground = Brushes.Black;
            Timer.Start();
            model.Time++;
            mainwindow.tb.textBlock.Text = $"Logged: {model.ShowTime()}";
        }
    }
}
