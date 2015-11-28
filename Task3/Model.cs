﻿using System;
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
using System.Text;
using System.Data.Objects;
using System.Globalization;
using System.Data.EntityClient;
using System.Data.Common;

namespace Task3
{
    public class TaskModel : INotifyPropertyChanged
    {
        int _id;
        private int _time = 0;
        private string _name = null;
        bool _timerIsActive = false;
        TaskInfo _task;
        TaskInfoContext _taskEntities;
        DispatcherTimer _timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel(int id)
        {
            _id = id;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _taskEntities = new TaskInfoContext();
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
        public int ModelTaskID { get { return _id; } }
        public string TaskTimerText
        {
            get { return ShowTime(); }
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
            if (Time != 0 && Time % 3 == 0)
            {
                UpdateSession(ModelTaskID, Time, Name);
            }
        }
        public int GetID()
        {
            return _id;
        }
        /// <summary>
        /// Edds a new task session to the DB
        /// </summary>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public void AddSession(int time, string name)
        {
            _taskEntities.TaskDataEntities.Add(new TaskInfo() { Name = name, TrackedTime = time, TaskBoxID = _id });
            _taskEntities.SaveChanges();
            var list = _taskEntities.TaskDataEntities.ToList();
            var tmp_message = "";
            foreach (var item in list)
            {
                tmp_message += $"ID:{item.TaskBoxID} Time:${item.TrackedTime} Name:{item.Name}\n";
            }
            MessageBox.Show(tmp_message);
        }

        /// <summary>
        /// Edits current task session in DB
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

            task.Name = this.Name;
            task.TrackedTime = this.Time;
            MessageBox.Show("Task saved.\n Name:" + task.Name + "\n TaskID:" + task.TaskBoxID + "\n TaskTime:" + task.TrackedTime);

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