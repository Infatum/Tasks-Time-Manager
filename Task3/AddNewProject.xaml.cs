using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Windows.Documents;

namespace Task3
{
    /// <summary>
    /// Interaction logic for AddNewProject.xaml
    /// </summary>
    public partial class AddNewProject : Window, INotifyPropertyChanged
    {
        ProjectDescription _currentProject;
        string _name;
        string _description;
        Projects _projectsUI;
        List<ProjectDescription> _projects;
        ProjectDescriptionModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        public AddNewProject()
        {
            InitializeComponent();
            _currentProject = new ProjectDescription();
            _projects = new List<ProjectDescription>();
            
        }
        public string Name
        {
            get { return textBoxName.Text; }
           
        }
        public string Description
        {
            get {
                _description = new TextRange(projectDescriptionTb.Document.ContentStart, projectDescriptionTb.Document.ContentEnd).Text;
                return _description;
            }
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            if (Name == null) 
            {
                MessageBox.Show("Fill in Name field");
                return;
            }
            _currentProject = new ProjectDescription { ProjectDescriptionText = new TextRange(projectDescriptionTb.Document.ContentStart, projectDescriptionTb.Document.ContentEnd).Text, ProjectName = textBoxName.Text };           
            foreach (var item in Application.Current.Windows)
            {
                if (item.GetType() == typeof(Projects))
                    _projectsUI = (Projects)item;
            }
            if (_projectsUI == null)
            {
                _projectsUI = new Projects();
                _projectsUI.Show();
            }
            _model = new ProjectDescriptionModel();
            _model.InsertSession(_currentProject);
            //_projectsUI.ListOfProjects.Add(_currentProject);
            //_projectsUI.AddingToNameAndDescriptionList(_projectsUI.ListOfProjects);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class User
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
