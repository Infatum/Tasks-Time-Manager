﻿using System.Collections.Generic;
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
        //public string ProjectNameText { get { return _name; } set { _name = value; } }
        //public string Name
        //{
        //    get { return _name; }
        //    set
        //    {
        //        _name = value;
        //        NotifyPropertyChanged("ProjectNameText");
        //    }
        //}
        //public string ProjectDescriptionText { get { return _description; } set { _description = value; } }
        //public string Description
        //{
        //    get { return _description; }
        //    set
        //    {
        //        _description = value;
        //        NotifyPropertyChanged("ProjectDescriptionText");
        //    }
        //}
        //public void NotifyPropertyChanged(string propName)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}


        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            if (Name == null) // FIXME: Show message on form (window)
            {
                MessageBox.Show("Fill in Name field");
                return;
            }
            //if (Description == null) // Empty description
            //    Description = "";

            //List<ProjectDescription> _projects = new List<ProjectDescription>();
            _currentProject = new ProjectDescription { ProjectDescriptionText = new TextRange(projectDescriptionTb.Document.ContentStart, projectDescriptionTb.Document.ContentEnd).Text, ProjectName = textBoxName.Text };
            
            foreach (var item in Application.Current.Windows)
            {
                if (item.GetType() == typeof(Projects))
                    _projectsUI = (Projects)item;
            }
            //_projects.Add(_currentProject);
            
            if (_projectsUI == null)
            {
                _projectsUI = new Projects();
                _projectsUI.Show();
            }
            _projectsUI.ListOfProjects.Add(_currentProject);
            _projectsUI.AddingToNameAndDescriptionList(_projectsUI.ListOfProjects);
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
