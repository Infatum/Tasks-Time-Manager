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
    public class TaskModel : INotifyPropertyChanged
    {
        int _taskID;
        private int _time = 0;
        private string _name = null;
        private bool _freelanceMode;
        private ProjectDescription _currentProj;
        private ProjectInfoContext _taskEntities = new ProjectInfoContext();
        private TaskInfo _currentTask;
        private DispatcherTimer _timer;
        private float _rate;
        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel() { }
        public TaskModel(int taskBoxID)
        {
            _taskID = taskBoxID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        public TaskModel(int taskBoxID, ProjectDescription project)
        {
            TaskInfo _currentTask = new TaskInfo();
            _currentTask.Task_Id = taskBoxID;
            _currentTask.TaskBoxID = taskBoxID;
            _currentTask.Project = project;
            _currentProj= project;
            //_taskID = taskBoxID;
            _timer = new DispatcherTimer();
            //_currentProj = project;
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

        public TaskModel(int taskID, int logged, string title, float rate)
        {
            _taskID = taskID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            Time = logged;
            Name = title;
            HourRate = rate;
        }

        public bool FreelanceMode
        {
            get { return _freelanceMode; }
            set
            {
                _freelanceMode = value;
                NotifyPropertyChanged("FreelancerMode");
            }
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

        public ProjectDescription CurrentProject
        {
            get { return _currentProj; }
        }

        public float TaskRateText { get { return _rate; } }
        public int TaskID { get { return _taskID; } }

        public TaskInfo CurrenntTaskEntity
        {
            get
            {
                if (_currentTask == null)
                {
                    _currentTask = new TaskInfo();
                }
                return _currentTask;
            }
        }
    

        public void InsertSession(int taskID, ProjectDescription project)
        {
            _currentTask = new TaskInfo();
            _currentTask.Task_Id = taskID;
            _currentTask.Project = project;
            _taskEntities.TaskDataEntities.Add(_currentTask);
            _taskEntities.SaveChanges();
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