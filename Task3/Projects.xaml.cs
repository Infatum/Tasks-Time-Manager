using System;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
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
        public Projects()
        {
            _projects = new ObservableCollection<ProjectTasks>();
            //_projectUI = new ProjectTasks();
            InitializeComponent();
        }

        private void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            _currentProject = new ProjectDescription();
            listViewProjectsNamesAndDescriptions.Items.Add(_currentProject);
        }
    }
}
