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
        ProjectInfoContext _taskEntities;
        ProjectDescription _currentProject;
        DispatcherTimer _timer;
        List<TaskInfo> _currentProjectTasks;
        private ICollection<TaskInfo> _tasks;
        float _rate;
        public event PropertyChangedEventHandler PropertyChanged;

        public TaskModel() { }
        public TaskModel(int taskBoxID, ProjectDescription project)
        {
            _currentProject = project;
            _taskID = taskBoxID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
        }
        public TaskModel(int taskBoxID, float hourRate,  ProjectDescription project)
        {
            _currentProject = project;
            _taskID = taskBoxID;
            HourRate = hourRate;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        public TaskModel(int taskBoxID, int projectID)
        {
            _currentProjectTasks = (List<TaskInfo>)LoadSession(projectID);
            _taskID = taskBoxID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
        }

        public TaskModel(int taskID, int logged, string title, ProjectDescription project)
        {
            _taskID = taskID;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            Time = logged;
            Name = title;
        }

        public bool TimerIsActive { get; set; }
        public List<TaskInfo> ProjectTasks { get { return _currentProjectTasks; } }
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
        public int TaskID { get { return _taskID; } }

        public TaskInfo CurrenntTaskEntity
        {
            get
            {
                var task = GetByTaskBoxId(_taskID);
                return task;
            }
        }

        public TaskInfo CurrentTaskEntity { get; set; }
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

        public ICollection<TaskInfo> LoadSession(int projectID)
        {

            using (var context = new ProjectInfoContext())
            {
                var projectWithTasks = context.Projects
                                    .Where(p => p.ProjectId == projectID)
                                    .Include("ProjectTasks")
                                    .FirstOrDefault();

                _currentProject = projectWithTasks;
                _tasks = projectWithTasks.ProjectTasks;
                return _tasks;
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
            if (GetByTaskBoxId(_taskID) == null)
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

        /// <summary>
        /// Finds the task entity in DB within giving taskBox ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> returns a TaskInfo object in case, when finds it in the DataBasw by the ID of a taskBox, if doesn't - evaluates NullReferenceException</returns>
        public TaskInfo GetByTaskBoxId(int id)
        {
            TaskInfo task = null;
            foreach (var t in _tasks)
            {
                if (t.TaskBoxID == id)
                {
                    task = t;
                    _taskID = t.TaskBoxID;
                }
            }
            return task;
        }

        public void InsertSession(TaskInfo entity)
        {
                try
                {
                    _currentProject.ProjectTasks.Add(entity);
                }
                catch (Exception ex)
                {

                    _currentProject.ProjectTasks = new List<TaskInfo>();
                }
                finally
                {
                    _taskEntities.SaveChanges();
                }
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