using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;

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
        Dictionary<string, string> _projectNameAndDescription;
        public event PropertyChangedEventHandler PropertyChanged;
        public AddNewProject()
        {
            InitializeComponent();
            _currentProject = new ProjectDescription();
            _projectNameAndDescription = new Dictionary<string, string>();
            this.DataContext = this;
        }
        public string ProjectNameText { get { return _name; } set { _name = value; } }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("ProjectNameText");
            }
        }
        public string ProjectDescriptionText { get { return _description; } set { _description = value; } }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("ProjectDescriptionText");
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            if (Name == null) // FIXME: Show message on form (window)
            {
                MessageBox.Show("Fill in Name field");
                return;
            }
            if (Description == null) // Empty description
                Description = "";
            _currentProject = new ProjectDescription { ProjectName = Name, ProjectDescriptionText = Description };
            _projectsUI = new Projects();
            _projectNameAndDescription.Add(Name, Description);
            _projectsUI.AddingToNameAndDescriptionList(_projectNameAndDescription);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
