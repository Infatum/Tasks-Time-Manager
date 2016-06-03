using System;
using System.Windows;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TasksTracker.Model.ConcreteLayer;
using TasksTracker.Model.AbstractLayer;
using TasksTracker.Data;
using Task3.Components;


namespace Task3.ViewModel
{
    public class TaskBoxViewModel : INotifyPropertyChanged
    {
        int _taskID;
        private int _time = 0;
        private string _name = null;
        private DispatcherTimer _timer;
        private float _rate;
        private List<TaskInfo> tasks;
        private bool _hourRateTextBoxIsEnebled = false;
        private ITaskDataAccesLayer<TaskInfo> _currentTaskModel;
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Constructor for Loading tasks and visualizing them on UI
        /// </summary>
        /// <param name="taskModel"></param>
        public TaskBoxViewModel(bool freelanceMode, ITaskDataAccesLayer<TaskInfo> taskModel, IProjectDatatAccesLayer<ProjectDescription> projectModel)
        {
            if (freelanceMode)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = new TimeSpan(0, 0, 1);
                tasks = taskModel.GetAllTasksInProject((ProjectDescription)projectModel.CurrentProject).ToList();
                _hourRateTextBoxIsEnebled = true;
            }
            _currentTaskModel = taskModel;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            tasks = taskModel.GetAllTasksInProject((ProjectDescription)projectModel.CurrentProject).ToList();
        }

        public bool TimerIsActive { get; set; }
        public ICollection<TaskInfo> ProjectTasks { get { return tasks; } }
        public bool HourRateIsEnabled { get { return _hourRateTextBoxIsEnebled; } }
        public int TaskID { get { return _taskID; } }
        public int TrackedTime { get { return Int32.Parse(TaskTimerText); } }
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("TaskTimerText");
            }
        }
        public string TaskTimerText
        {
            get { return ShowTime(_time); }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("TaskNameText");
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
        public float HourRate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                NotifyPropertyChanged("HourRateText");
            }
        }

        public string HourRateText
        {
            get { return _rate.ToString(); }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void UpdateSession(ITaskDataAccesLayer<TaskInfo> taskModel)
        {
            if (HourRateIsEnabled)
            {
                taskModel.Update(_taskID, Name, TrackedTime, HourRateIsEnabled, HourRate);
            }
            else
            {
                taskModel.Update(_taskID, Name, TrackedTime);
            }
        }

        public void UpdateSession(IProjectDatatAccesLayer<TaskInfo> projectModel)
        {

        }

        public void UpdateSession(int taskID)
        {

        }
        /// <summary>
        /// The  timer performance
        /// </summary>
        /// <returns></returns>
        public static string ShowTime(int time)
        {
            return $"Logged: {time / 3600}:{time % 3600 / 60}:{time % 60}";
        }

        /// <summary>
        /// Timer tick for updating data storage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            Time++;
            if (Time != 0 && Time % 3 == 0)
            {
                UpdateSession(_currentTaskModel);
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
