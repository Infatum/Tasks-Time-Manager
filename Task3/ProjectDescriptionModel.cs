using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public ObservableCollection<TaskInfo> ProjectTasks { get; set; }
       

        /// <summary>
        /// Deletes the Project with all Tasks in it
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSession(int id)
        {
            using (var cntx = new ProjectInfoContext())
            {
                DbModelBuilder modelbuilder = new DbModelBuilder();
                modelbuilder.Entity<TaskInfo>().HasRequired(x => x.Project)
                    .WithMany()
                    .WillCascadeOnDelete(true);
                var project = from p in cntx.Projects where p.ProjectId == id select p;
                    cntx.Projects.Remove((ProjectDescription)project);
                cntx.SaveChanges();
            }
            
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
            using (var cntx = new ProjectInfoContext())
            {
                cntx.Projects.Add(entity);
                cntx.SaveChanges();
            }
        }

        public void UpdateSession(ProjectDescription entity)
        {
            using (var cntx = new ProjectInfoContext())
            {
                if (GetById(entity.ProjectId) != null)
                {
                    MessageBox.Show("No such Project found");
                    return;
                }
                entity.ProjectName = Name;
                entity.ProjectTasks = ProjectTasks;
                cntx.SaveChanges();
            }
            
        }
    }
}
