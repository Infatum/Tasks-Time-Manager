using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;

namespace Task3
{
    public class Model : INotifyPropertyChanged
    {
        private int _time = 0;
        bool _timerIsActive = false;
        Task _task;
        DispatcherTimer _timer;
        public event PropertyChangedEventHandler PropertyChanged;

        public Model()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1); // Забула ось цю строку, тому таймер працював швидко
        }
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("TaskTimerText"); // sAY TO ALL THAT data changed, and need find it in TaskTimerText
            }
        }
        public string TaskTimerText
        {
            get { return ShowTime(); } // Return formatted text to "listeners" who receive PropertyChanged "signal"
        }

        public DateTime ProjectStartTime { get; set; }
        public int Timer { get; set; }
        public TimeSpan TrackedTime { get; set; }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Reset timer
        /// </summary>
        public void ResetTime()
        {
            _time = 0;
        }
        /// <summary>
        /// The  timer performance
        /// </summary>
        /// <returns></returns>
        public string ShowTime()
        {
            return $"Logged {_time / 3600}:{this._time % 3600 / 60}:{this._time % 60}";
        }

        /// <summary>
        /// Timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            Time++;
        }

        /// <summary>
        /// Starts/resumes timer
        /// </summary>
        public void StartResumeTimer()
        {
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        /// <summary>
        /// Pauses or/and stops the timer
        /// </summary>
        public void StopPauseTimer()
        {
            _timer.Tick -= Timer_Tick;
            _timer.Stop();
        }
    }
}