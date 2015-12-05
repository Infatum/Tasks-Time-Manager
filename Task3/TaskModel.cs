using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Text;
using System.Data.Objects;
using System.Globalization;
using System.Data.EntityClient;
using System.Data.Common;

namespace Task3
{
    public class TaskModel : INotifyPropertyChanged
    {
        int _taskID;
        private int _time = 0;
        private string _name = null;
        TaskInfoContext _taskEntities;
        DispatcherTimer _timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel() { }
        public TaskModel(int taskBoxID)
        {
            _taskID = taskBoxID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        public TaskModel(int taskID, int logged, string title)
        {
            _taskID = taskID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            Time = logged;
            Name = title;
        }

        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("TaskTimerText");
            }
        }

        public int ModelTaskID { get { return _taskID; } }

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

        /// <summary>
        /// 
        /// </summary>
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
        public static string ShowTime(int time)
        {
            return $"Logged {time / 3600}:{time % 3600 / 60}:{time % 60}";
        }

        /// <summary>
        /// Timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            Time++;
            if (Time != 0 && Time % 3 == 0)
            {
                UpdateSession(ModelTaskID, Time, TaskNameText);
            }
        }

        public int GetID()
        {
            return _taskID;
        }
        public void CreateDB()
        {
            _taskEntities = new TaskInfoContext();
        }

        public TaskInfoContext DBContext { get { return _taskEntities; } }

        /// <summary>
        /// 
        /// </summary>
        public void EndSession()
        {
            using (_taskEntities)
            {
                UpdateSession(_taskID, Time, Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadSession()
        {
            CreateDB();
            if (_taskEntities.TaskDataEntities.Count() > 0)
            {
                _taskEntities.TaskDataEntities.Load();
            }
        }

        /// <summary>
        /// Edds a new task session to the DB
        /// </summary>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public void AddSession(int time, string name)
        {
            _taskEntities.TaskDataEntities.Add(new TaskInfo() { Name = name, TrackedTime = time, TaskBoxID = _taskID });
            _taskEntities.SaveChanges();
        }

        /// <summary>
        /// Writing task session changes to DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public void UpdateSession(int id, int time, string name)
        {
            var task = (from t in _taskEntities.TaskDataEntities where t.TaskBoxID == id select t).First();

            if (task == null)
            {
                MessageBox.Show("ERROR: Task not found in DB. Skip savig...");
                return;
            }
                task.Name = name;
                task.TrackedTime = time;
                _taskEntities.SaveChanges();
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