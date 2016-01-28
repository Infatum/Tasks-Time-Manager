using System;
using System.Data.Entity;
using System.Linq;

namespace Task3
{

    public class ProjectInfoContext : DbContext
    {
        public ProjectInfoContext()
            : base("TaskInfo.ProjectInfoContext")
            //base("name=TaskData")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TaskInfo>().HasRequired(p => p.Project)
        //        .WithMany(t => t.ProjectTasks)
        //        .HasForeignKey(fk => fk.FKTask_Id);

        //}

        public virtual DbSet<TaskInfo> TaskDataEntities { get; set; }
        public virtual DbSet<ProjectDescription> Projects { get; set; }
    }
}