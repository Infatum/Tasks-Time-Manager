using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ProjectDescriptionModel : IRepository<ProjectDescription>, INotifyPropertyChanged

    {
        private string _name;
        private string _description;
        public event PropertyChangedEventHandler PropertyChanged;
        

        public string ProjectNameText
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("ProjectNameText");
            }
        }
        public string ProjectDescriptionText { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                _name = value;
                NotifyPropertyChanged("ProjectDescriptionText");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) 
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

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
