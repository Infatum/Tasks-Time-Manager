using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksTracker.Data;

namespace TasksTracker.Model.AbstractLayer
{
    public interface ITaskDataAccesLayer<T> where T : class
    {
        int ProjectID { get; }
        void Insert(int taskId);
        void Remove(int taskId);
        void Update(int taskID, string name = "", int trackedTime = 0, bool freeLanceMode = false, float hourRate = 0f);
        ProjectDescription GetCurrentProject(TaskInfo entity);
        ICollection<TaskInfo> GetAllTasksInProject(int projectId);
        ICollection<TaskInfo> GetAllTasksInProject(ProjectDescription project);
        ICollection<TaskInfo> GetAllTasksInProjectByTaskId(int taskId);
        TaskInfo GetTaskByID(int taskID);
        ProjectDescription GetProjectByTaskId(int taskId);
        int GetMaxTaskID();
    }
}
