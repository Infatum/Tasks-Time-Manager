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
        ObservableCollection<ProjectTasks> _projects;
        ProjectInfoContext _db;
        ProjectDescription _currentProject;
        ProjectTasks _projectUI;
        AddNewProject _newProjectWindow;
        
        public Projects()
        {
            _projects = new ObservableCollection<ProjectTasks>();
            //_projectNameAndDescription = new Dictionary<string, string>();
            //_projectUI = new ProjectTasks();
            InitializeComponent();
        }
        public void AddingToNameAndDescriptionList(Dictionary<string, string> projectNameDescr)
        {
            listViewProjectsNamesAndDescriptions.ItemsSource = projectNameDescr;
            //MessageBox.Show(projectNameDescr[_newProjectWindow.Name]);
        }
        private void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            _currentProject = new ProjectDescription();
            _newProjectWindow = new AddNewProject();
            _newProjectWindow.Show();
        }
    }
}
