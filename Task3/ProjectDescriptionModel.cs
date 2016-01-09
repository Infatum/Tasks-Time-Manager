using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ProjectDescriptionModel : IRepository<ProjectDescription>
    {
        public ICollection<ProjectDescription> LoadSession()
        {
            using (var context = new ProjectInfoContext())
            {
                return context.Projects.ToList();
            }
        }
        public void DeleteSession(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectDescription GetById(int id)
        {
            using (var context = new ProjectInfoContext())
            {
                var project = context.Projects.FirstOrDefault(p => p.ProjectId == id);
                if (project != null)
                {
                    context.Entry(project)
                        .Reference(p => p.ProjectTasks)
                        .Load();
                }
                return project;
            }
        }

        public void InsertSession(ProjectDescription entity)
        {
            
        }

        public void UpdateSession(ProjectDescription entity)
        {
            throw new NotImplementedException();
        }
    }
}
