using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Data.Entity;
using TasksTracker.Model.AbstractLayer;
using TasksTracker.Data;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TasksTracker.Model.ConcreteLayer;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Task3.ViewModel
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        private string _project_name;
        private string _description;
        private bool _freelanceMode = false;
        IProjectDatatAccesLayer<ProjectDescription> _projectModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectViewModel()
        {
            _projectModel = CreateProjectModel();
        }
        public ProjectViewModel(string projectName, string description)
        {
            _projectModel = CreateProjectModel();
            _project_name = projectName;
            _description = description;
        }

        public string ProjectNameText
        {
            get { return _project_name; }
            set
            {
                _project_name = value;
            }
        }
        public string Name
        {
            get { return _project_name; }
            set
            {
                _project_name = value;
                NotifyPropertyChanged("ProjectNameText");
            }
        }
        public string ProjectDescriptionText { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("ProjectDescriptionText");
            }
        }

        public bool FreelanceMode
        {
            get { return _freelanceMode; }
            set
            {
                _freelanceMode = value;
                NotifyPropertyChanged("ProjectDescriptionText");
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void AddProject()
        {
            if (_projectModel == null)
            {
                _projectModel = CreateProjectModel();
            }
            _projectModel.Insert(new ProjectDescription
                                { ProjectName = Name, ProjectDescriptionText = Description,
                                  FreelanceMode = _freelanceMode });
        }

        public void DeleteProject(int projectID)
        {
            if (_projectModel == null)
            {
                _projectModel = CreateProjectModel();
            }
            _projectModel.Remove(projectID);
        }

        private IProjectDatatAccesLayer<ProjectDescription> CreateProjectModel()
        {
            _projectModel = new MSSQLServerProjectModel() as IProjectDatatAccesLayer<ProjectDescription>;
            return _projectModel;
        }

        public ObservableCollection<ProjectDescription> LoadProjects()
        {
            ObservableCollection<ProjectDescription> projectCollection = new ObservableCollection<ProjectDescription>();
            if (_projectModel == null)
            {
                _projectModel = CreateProjectModel();
            }

            var projectsLoadedFromDB = _projectModel.GetAllProjects();
            foreach (var p in projectsLoadedFromDB)
            {
                projectCollection.Add(p);
            }
            return projectCollection;
        }
    }
}
