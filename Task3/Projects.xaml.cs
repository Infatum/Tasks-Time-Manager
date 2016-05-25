using System;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task3
{
    /// <summary>
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        ObservableCollection<ProjectDescription> _projects;
        ProjectDescription _currentProject;
        ProjectDescriptionModel _model;
        ProjectTasks _taskWindow;
        AddNewProject _newProjectWindow;

        public ObservableCollection<ProjectDescription> ProjectsListDataSource { get { return _projects; } }
        public Projects()
        {
            _model = new ProjectDescriptionModel();
            _projects = new ObservableCollection<ProjectDescription>();
  
            InitializeComponent();
            LoadProjects(_model);
            this.DataContext = this;
        }

        public ObservableCollection<ProjectDescription> ListOfProjects
        {
            get { return _projects; }
        }

        private void LoadProjects(ProjectDescriptionModel model)
        {
           var projects = model.LoadSession();
            foreach (var proj in projects)
            {
                this.ListOfProjects.Add(proj);
            }
            AddingToNameAndDescriptionList(this.ListOfProjects);
        }

        public void AddingToNameAndDescriptionList(ObservableCollection<ProjectDescription> projects)
        {
            foreach (var item in projects)
            {
                MessageBox.Show(item.ProjectName + " " + item.ProjectDescriptionText);
            }
            this.listViewProjectsNamesAndDescriptions.ItemsSource = projects;
        }

        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as ProjectDescription;
            if (item != null)
            {
                _taskWindow = new ProjectTasks();
                _taskWindow.Show();
            }
        }

        private void OnLoad()
        {

        }
        private void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            _currentProject = new ProjectDescription();
            _newProjectWindow = new AddNewProject();
            _newProjectWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
