using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksTracker.Data;
using TasksTracker.Model.AbstractLayer;
using System.Windows;
using System.Data;
using System.Data.Entity;

namespace TasksTracker.Model.ConcreteLayer
{
    public class MSSQLServerTaskModel : ITaskDataAccesLayer<MSSQLServerTaskModel>
    {
        private ProjectInfoContext _taskEntities;
        private ProjectDescription _currentProject;
        private List<TaskInfo> _currentProjectTasks;
        private ProjectInfoContext _dataBaseContext;
        private int _taskID;

        public ProjectDescription CurrentProject { get { return _currentProject; } }
        public int ProjectID { get { return _currentProject.ProjectId; } }
        public List<TaskInfo> AllProjectTasks
        {
            get
            {
                try
                {
                    _currentProjectTasks = GetAllTasksInProject(_currentProject).ToList();
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return GetAllTasksInProject(_currentProject).ToList();
            }
        }

        public MSSQLServerTaskModel(ProjectDescription project)
        {
            _currentProject = project;
        }
        public MSSQLServerTaskModel(int taskID)
        {
            _taskID = taskID;
            _currentProject = GetProjectByTaskId(taskID);
        }
        public MSSQLServerTaskModel(int taskID, ProjectDescription project)
        {
            _currentProject = project;
            _taskID = taskID;
        }

        //TODO:
        public MSSQLServerTaskModel(MSSQLServerProjectModel projectModel, int taskID)
        {
        }

        public ICollection<TaskInfo> GetAllTasksInProject(ProjectDescription project)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                var projectWithTasks = _dataBaseContext.Projects
                                     .Where(p => p.ProjectId == project.ProjectId)
                                     .Include("ProjectTasks")
                                     .FirstOrDefault();
                return projectWithTasks.ProjectTasks;
            }
        }

        public ICollection<TaskInfo> GetAllTasksInProject(int projectId)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                return _dataBaseContext.Projects.Find(projectId).ProjectTasks;
            }
        }

        public ICollection<TaskInfo> GetAllTasksInProjectByTaskId(int taskId)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
               return (_dataBaseContext.TaskDataEntities.Find(taskId).Project).ProjectTasks;
            }
        }

        public ProjectDescription GetProjectByTaskId(int taskId)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                var task = _dataBaseContext.TaskDataEntities.Find(taskId);
                var project = task.Project;
                return project;
            }
        }

        public ProjectDescription GetCurrentProject(TaskInfo entity)
        {
            return entity.Project;
        }

        public TaskInfo GetTaskByID(int taskID)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                return _dataBaseContext.TaskDataEntities.Find(taskID);
            }
        }

        public void Insert(int taskId)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                if (_currentProject.ProjectTasks == null)
                    _currentProject.ProjectTasks = new List<TaskInfo>();

                TaskInfo task = new TaskInfo() { Task_Id = taskId, Project = CurrentProject };
                _dataBaseContext.TaskDataEntities.Add(task);
                _dataBaseContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes current Task, without deleting the project
        /// </summary>
        /// <param name="id">Task ID</param>
        public void Remove(int taskID)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                DbModelBuilder modelBuilder = new DbModelBuilder();
                modelBuilder.Entity<TaskInfo>().HasRequired(x => x.Project)
                            .WithMany(x => x.ProjectTasks).WillCascadeOnDelete(false);

                var task = _dataBaseContext.TaskDataEntities.Find(taskID);
                _dataBaseContext.TaskDataEntities.Remove(task);
                _dataBaseContext.SaveChanges();
            }
        }
        /// <summary>
        /// Writing task session changes to DB
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <param name="time">Task Tracked Time </param>
        /// <param name="name">Task Name</param>
        public void Update(int taskID, string name = "", int trackedTime = 0,bool freelanceMode = false, float hourRate = 0)
        {
            if (GetTaskByID(taskID) == null)
            {
                MessageBox.Show("ERROR: Task not found in DB. Skip savig...");
                return;
            }

            using (_dataBaseContext = new ProjectInfoContext())
            {
                TaskInfo taskToUpdate = _dataBaseContext.TaskDataEntities.Find(taskID);
                ProjectDescription projectOfTheCurrTask = GetProjectByTaskId(taskID);
                if (taskToUpdate.Project == projectOfTheCurrTask)
                {
                    taskToUpdate.Name = name;
                    taskToUpdate.TrackedTime = trackedTime;
                    if (projectOfTheCurrTask.FreelanceMode)
                    {
                        taskToUpdate.HourRate = hourRate;
                    }
                }
                _dataBaseContext.SaveChanges();
            }
        }

        public int GetMaxTaskID()
        {
            try
            {
                using (_dataBaseContext = new ProjectInfoContext())
                {
                    return _dataBaseContext.Database.SqlQuery<int>
                     ("SELECT MAX(TaskBoxID) FROM TASKINFOES;").FirstOrDefault<int>();
                }
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        public void AssignTaskToProject(ProjectDescription project, TaskInfo task)
        {
            using (_dataBaseContext = new ProjectInfoContext())
            {
                task.Project = project;
                _dataBaseContext.SaveChanges();
            }
        }
    }
}
