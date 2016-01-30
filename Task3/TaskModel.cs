using System;
using System.Windows;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Task3
{
    public class TaskModel : INotifyPropertyChanged, IRepository<TaskInfo>
    {
        int _taskID;
        private int _time = 0;
        private string _name = null;
        ProjectInfoContext _taskEntities;
        DispatcherTimer _timer;
        float _rate;
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
                NotifyPropertyChanged("HourRate");
            }
        }

        public float TaskRateText { get { return _rate; } }
        public int ModelTaskID { get { return _taskID; } }

        public TaskInfo CurrenntTaskEntity
        {
            get
            {
                var task = GetById(_taskID);
                return task;
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
            return $"Logged: {time / 3600}:{time % 3600 / 60}:{time % 60}";
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
                UpdateSession(CurrenntTaskEntity);
            }
        }

        public int GetID()
        {
            return _taskID;
        }
        public void CreateDB()
        {
            _taskEntities = new ProjectInfoContext();
        }

        public ProjectInfoContext DBContext { get { return _taskEntities; } }

        public ICollection<TaskInfo> LoadSession()
        {
            using (var context = new ProjectInfoContext())
            {
                var tasks = context.TaskDataEntities.ToList();
                return tasks;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndSession()
        {
            using (_taskEntities)
            {
                UpdateSession(CurrenntTaskEntity);
            }
        }

        /// <summary>
        /// Writing task session changes to DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public void UpdateSession(TaskInfo entity)
        {
            if (GetById(_taskID) != null)
            {
                MessageBox.Show("ERROR: Task not found in DB. Skip savig...");
                return;
            }
            entity.Name = Name;
            entity.TrackedTime = Time;
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

        public TaskInfo GetById(int id)
        {
            var task = (from t in _taskEntities.TaskDataEntities where t.TaskBoxID == id select t).First();
            return task;
        }

        public void InsertSession(TaskInfo entity)
        {
            _taskEntities.TaskDataEntities.Add(entity);
            _taskEntities.SaveChanges();
        }

        /// <summary>
        /// Deletes current Task, without deleting the project
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSession(int id)
        {
            using (ProjectInfoContext cntx = new ProjectInfoContext())
            {
                DbModelBuilder modelBuilder = new DbModelBuilder();
                modelBuilder.Entity<TaskInfo>().HasRequired(x => x.Project)
                    .WithMany(x => x.ProjectTasks).WillCascadeOnDelete(false);
                var task = cntx.TaskDataEntities.Find(id);
                cntx.TaskDataEntities.Remove(task);
                cntx.SaveChanges();

            }
        }
    }
}