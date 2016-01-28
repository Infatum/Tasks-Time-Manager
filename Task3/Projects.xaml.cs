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
        AddNewProject _newProjectWindow;

        public ObservableCollection<ProjectDescription> ProjectsListDataSource { get { return _projects; } }
        public Projects()
        {
            _projects = new ObservableCollection<ProjectDescription>();
            //_projectNameAndDescription = new Dictionary<string, string>();
            //_projectUI = new ProjectTasks();
            InitializeComponent();
        }

        public ObservableCollection<ProjectDescription> ListOfProjects
        {
            get { return _projects; }
        }
        public void AddingToNameAndDescriptionList(ObservableCollection<ProjectDescription> projects)
        {
            foreach (var item in projects)
            {
                MessageBox.Show(item.ProjectName + " " + item.ProjectDescriptionText);
            }
            //listViewProjectsNamesAndDescriptions.ItemsSource = null;
            listViewProjectsNamesAndDescriptions.ItemsSource = projects;
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
