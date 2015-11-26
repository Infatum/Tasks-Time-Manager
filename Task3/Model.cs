using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;

namespace Task3
{
    public class TaskModel : INotifyPropertyChanged
    {
        private int _time = 0;
        private string _name = null;
        bool _timerIsActive = false;
        TaskInfo _task;
        TaskInfoData _taskEntities;
        DispatcherTimer _timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _taskEntities = new TaskInfoData();
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("TaskNameText"); // sAY TO ALL THAT data changed, and need find it in TaskTimerText
            }
        }

        public string TaskNameText
        {
            get { return _name; }
            set
            {
                _name = value;
            }
                
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
        
        public void AddSession(int time, string name)
        {
            _taskEntities.TaskDataEntities.Add(new TaskInfo() { Name = name, TrackedTime = time });
            _taskEntities.SaveChanges();
            var list = _taskEntities.TaskDataEntities.ToList();
            foreach (var item in list)
            {
                MessageBox.Show($"{item.Name} : ${item.TrackedTime}");
            }
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