using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksTracker.Model.AbstractLayer;
using TasksTracker.Model.ConcreteLayer;
using System.Windows.Forms;
using TasksTracker.Data;
using System.ComponentModel;

namespace Task3.ViewModel
{
    public class AddNewProjectViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        ProjectViewModel _projectsUI;
        IProjectDatatAccesLayer<ProjectDescription> _projectModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddNewProjectViewModel(IProjectDatatAccesLayer<ProjectDescription> projectModel)
        {
            _projectModel = projectModel;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string ProjectName
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("ProjectNameText");
            }

        }

        public string ProjectNameText
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("DescriptionText");
            }
        }

        public string DescriptionText
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private void AddProject()
        {
            if (ProjectName == null)
            {
                MessageBox.Show("Fill in Name field");
                return;
            }

            _projectModel.Insert(new ProjectDescription()
            {
                ProjectName = ProjectName,
                ProjectDescriptionText = Description
            });
            //_currentProject = new ProjectDescription
            //{
            //    ProjectDescriptionText = new TextRange(
            //        projectDescriptionTb.Document.ContentStart,
            //        projectDescriptionTb.Document.ContentEnd
            //    ).Text,
            //    ProjectName = textBoxName.Text
            //};

            //foreach (var item in Application.Current.Windows)
            //{
            //    if (item.GetType() == typeof(Projects))
            //        _projectsUI = (Projects)item;
            //}

            //if (_projectsUI == null)
            //{
            //    _projectsUI = new Projects();
            //    _projectsUI.Show();
            //}

            //_model = new ProjectDescriptionModel();
            //_model.InsertSession(_currentProject);
            //_projectsUI.ListOfProjects.Add(_currentProject);
            ////_projectsUI.ListOfProjects.Add(_currentProject);
            ////_projectsUI.AddingToNameAndDescriptionList(_projectsUI.ListOfProjects);
            //this.Close();
        }
    }
}
