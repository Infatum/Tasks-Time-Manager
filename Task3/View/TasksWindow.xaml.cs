using System;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Controls;

namespace TasksTracker
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProjectTaskWindow : Window
    {
        //ObservableCollection<TaskBox> _tasks;
        //public TaskBox tb = null;
        //private TaskModel _taskModel;
        //static int taskCounter = 0;
        //ProjectInfoContext _db;
        //ProjectDescription _currentProject;
        //float _rate;
        //bool _freelancerMode;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public static int TaskID { get { return taskCounter; } }

        //public ProjectTaskWindow(ProjectDescription project)
        //{
        //    _tasks = new ObservableCollection<TaskBox>();
        //    InitializeComponent();
        //    taskCounter = getMaxTaskID() + 1;
        //    _currentProject = project;
        //    _taskModel = new TaskModel(taskCounter, _currentProject);
        //    _taskModel.OpenDB();
        //    _taskModel.GetProjectTasks(project.ProjectId);
        //    LoadTaskWindows();
        //}

        //private int getMaxTaskID()
        //{
            
        //}

        //private void LoadTaskWindows()
        //{
        //    var savedSessions = _currentProject.ProjectTasks;
        //    foreach (TaskInfo session in savedSessions)
        //    {
        //        tb = new TaskBox
        //            (session.TaskBoxID, session.TrackedTime,
        //             session.Name, _rate, _currentProject);
        //        // TODO: FreelancerMode 
        //        tb.textBlockHorRate.Text = _rate.ToString();
        //        _tasks.Add(tb);
        //        tasksStackPanel.Children.Add(tb);
        //    }
        //}

        //private void btnAddTask_Click(object sender, RoutedEventArgs e)
        //{
        //    _taskModel = new TaskModel(taskCounter, _currentProject);
        //    _taskModel.OpenDB();
        //    _taskModel.InsertTask(new TaskInfo()
        //    {
        //        Task_Id = taskCounter,
        //        TaskBoxID = taskCounter,
        //        TrackedTime = 0,
        //        Project = _currentProject
        //    }, _currentProject);
        //    tb = new TaskBox(taskCounter, _currentProject);

        //    tb.ID = taskCounter;
        //    _tasks.Add(tb);
        //    tasksStackPanel.Children.Add(tb);
        //    taskCounter++;
        //    _taskModel.CloseDB();
        //}

        //private void btnAddGenerateTextReport_Click(object sender, RoutedEventArgs e)
        //{
        //    SavePDFDocument(SaveFileDialog());
        //}

      

        //private void FrellancerModeOn(object sender, RoutedEventArgs e)
        //{
        //    _freelancerMode = true;
        //    tbProjectRate.IsEnabled = true;
        //}

        //private void RateChanged(object sender, TextChangedEventArgs e)
        //{
        //    _rate = float.Parse(tbProjectRate.Text);
        //}
    }
}
