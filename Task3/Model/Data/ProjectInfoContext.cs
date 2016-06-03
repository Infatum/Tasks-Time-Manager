using System;
using System.Data.Entity;
using System.Linq;

namespace TasksTracker.Data
{

    public class ProjectInfoContext : DbContext
    {
        public ProjectInfoContext()
            : base("TaskInfo.ProjectInfoContext")
        {
        }

        public virtual DbSet<TaskInfo> TaskDataEntities { get; set; }
        public virtual DbSet<ProjectDescription> Projects { get; set; }
    }
}