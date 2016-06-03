using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksTracker.Data;

namespace TasksTracker.Model.AbstractLayer
{
    public interface IProjectDatatAccesLayer<T> where T : class
    {
        void Insert (ProjectDescription newProject);
        void Remove (int entityID);
        void Update 
                    (int projectId, ICollection<TaskInfo> tasks, string name = "", 
                    string description = "", bool freelanceMode = false);
        IProjectDatatAccesLayer<ProjectDescription> CurrentProject { get; set; }
        ICollection<TaskInfo> GetCurrentProjectTasks (ProjectDescription entity);
        ICollection<ProjectDescription> GetAllProjects ();
        ProjectDescription GetProjectByID (int projectID);
    }
}
