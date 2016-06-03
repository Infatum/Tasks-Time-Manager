using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Collections.ObjectModel;
using TasksTracker.Data;
using TasksTracker.Model.AbstractLayer;

namespace TasksTracker.Model.ConcreteLayer
{
    public class MSSQLServerProjectModel : IProjectDatatAccesLayer<MSSQLServerProjectModel>
    {
        private ProjectInfoContext _dbContext;
        private ICollection<ProjectDescription> _allProjects;

        public IProjectDatatAccesLayer<ProjectDescription> CurrentProject
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MSSQLServerProjectModel()
        {
            _allProjects = GetAllProjects();               
        }

        public ICollection<ProjectDescription> GetAllProjects()
        {
            using (_dbContext = new ProjectInfoContext())
            {
                return  _dbContext.Projects
                        .Include(p => p.ProjectTasks)
                        .ToList();
            }
        }

        /// <summary>
        /// Find Project in DataBase by ID
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>If finds a seeking Id in the Project DataTable - Returns a seeked project, in other case evaluates Null reference exception</returns>
        public ProjectDescription GetProjectByID(int projectID)
        {
            ProjectDescription project = null;
            var projects = GetAllProjects();
            foreach (var proj in projects)
            {
                if (proj.ProjectId == projectID)
                {
                    project = proj;
                }
            }
            return project;
        }

        public ICollection<TaskInfo> GetCurrentProjectTasks(ProjectDescription project)
        {
            using (_dbContext = new ProjectInfoContext())
            {
                var selectedProject = _dbContext.Projects
                            .Where(p => p.ProjectId == project.ProjectId)
                            .Include("ProjectDescription")
                            .FirstOrDefault();
                var tasks = selectedProject.ProjectTasks;
                return tasks;
            }
        }

        public void Insert(ProjectDescription newProject)
        {
            using (_dbContext = new ProjectInfoContext())
            {
                _dbContext.Projects.Add(newProject);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the Project with all Tasks in it
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int projectID)
        {
            using (_dbContext = new ProjectInfoContext())
            {
                DbModelBuilder modelbuilder = new DbModelBuilder();
                modelbuilder.Entity<TaskInfo>().HasRequired(x => x.Project)
                    .WithMany()
                    .WillCascadeOnDelete(true);

                var project = from p in _dbContext.Projects where p.ProjectId == projectID select p;
                _dbContext.Projects.Remove((ProjectDescription)project);
                _dbContext.SaveChanges();
            }
        }

        public void Update(int projectId, ICollection<TaskInfo> tasks, string name = "", string description = "", bool freelanceMode = false)
        {
            try
            {
                var project = GetProjectByID(projectId);
                using (_dbContext = new ProjectInfoContext())
                {
                    project.ProjectTasks = tasks;
                    project.ProjectName = name;
                    project.ProjectDescriptionText = description;
                    project.FreelanceMode = freelanceMode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        public ProjectDescription GetByTaskId(int taskId)
        {
            using (_dbContext = new ProjectInfoContext())
            {
                var project = _dbContext.TaskDataEntities.Find(taskId).Project;
                if (project != null)
                {
                    _dbContext.Entry(project)
                        .Reference(p => p.ProjectTasks)
                        .Load();
                }
                return project;
            }
        }
    }
}
