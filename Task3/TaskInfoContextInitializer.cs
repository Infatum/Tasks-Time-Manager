using SQLite.CodeFirst;
using System.Data.Entity;

namespace Task3
{
    public class TaskInfoContextInitializer : SqliteDropCreateDatabaseAlways<TaskInfoContext>
    {
        public TaskInfoContextInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder) { }

        protected override void Seed(TaskInfoContext context)
        {
            context.Set<TaskInfo>().Add(new TaskInfo());
        }

    }
}
