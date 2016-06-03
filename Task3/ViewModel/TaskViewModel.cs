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
    public class TaskViewModel : INotifyPropertyChanged
    {
        private float _rate;
        private int taskID;
        private bool _freelanceMode;
        private ITaskDataAccesLayer<TaskViewModel> _taskModel;
        private IProjectDatatAccesLayer<ProjectDescription> _projectModel;
        private ICollection<TaskInfo> _tasks;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creating a new TaskViewModel instance with a new TaskInfo object and saves it to Data Storage
        /// </summary>
        /// <param name="itaskModel"></param>
        /// <param name="taskID"></param>
        public TaskViewModel(int projectID)
        {
            _taskModel = (ITaskDataAccesLayer<TaskViewModel>)new MSSQLServerTaskModel(taskID);
            _taskModel.Insert(projectID);
            _tasks = new List<TaskInfo>();
            var proj = _projectModel.GetProjectByID(projectID);
            _tasks.Add(new TaskInfo { Project = proj });
            InitializeLoadedDataToTheView(_tasks);
        }

        /// <summary>
        /// Creates TaskViewModel instance and Loads All available tasks from data storage
        /// </summary>
        /// <param name="itaskModel"></param>
        /// <param name="projectModel"></param>
        public TaskViewModel(bool itaskModel, IProjectDatatAccesLayer<ProjectDescription> projectModel)
        {
            if (itaskModel == true)
            {
                _taskModel = (ITaskDataAccesLayer<TaskViewModel>)
                    new MSSQLServerTaskModel(projectModel.CurrentProject as ProjectDescription);
                _projectModel = (IProjectDatatAccesLayer<ProjectDescription>)new MSSQLServerProjectModel();
                _tasks = _projectModel.GetAllProjects() as List<TaskInfo>;
            }
            else
            {
                _taskModel = (ITaskDataAccesLayer<TaskViewModel>)
                   new MSSQLServerTaskModel(projectModel.CurrentProject as ProjectDescription);
                _tasks = _taskModel.GetAllTasksInProject(projectModel.CurrentProject as ProjectDescription);
            }
            InitializeLoadedDataToTheView(_tasks);
         }

        public void InitializeLoadedDataToTheView(ICollection<TaskInfo> tasks)
        {
            List<TaskBoxViewModel> taskBoxesItems = new List<TaskBoxViewModel>();
            var taskModel = _taskModel as ITaskDataAccesLayer<TaskInfo>;
            foreach (var t in tasks)
            {
                taskBoxesItems.Add(new TaskBoxViewModel(FreelanceMode, taskModel, _projectModel));
            }
        }

        public bool FreelanceMode
        {
            get { return _freelanceMode; }
            set
            {
                _freelanceMode = value;
                NotifyPropertyChanged("TaskTimerText");
            }
        }
       
        public float TaskRateText { get { return _rate; } }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
