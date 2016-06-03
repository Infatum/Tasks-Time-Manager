using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using Task3.ViewModel;
using Task3.Components;


namespace TasksTracker
{
    /// <summary>
    /// Interaction logic for TaskBox.xaml
    /// </summary>
    public partial class TaskBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TaskBoxViewModel _taskBoxViewModel;
        bool _timerIsActive;

        //public TaskBox(int taskID, ProjectDescription project)
        //{
        //    InitializeComponent();
        //    textBlock.Tag = ID;
        //    this._model = new TaskModel(taskID, project);
        //    //_model.CreateDB();
        //    this.DataContext = this._model;
        //}


        //public TaskBox(int taskID, string name, int loggedTime, ProjectDescription project)
        //{
        //    InitializeComponent();
        //    textBlock.Tag = ID;
        //    this._model = new TaskModel(taskID, project);
        //    //_model.CreateDB();
        //    this.DataContext = this._model;
        //}

        ///// <summary>
        ///// Creating a TaskBox item with enebled Freelancer Mode and hour rate
        ///// </summary>
        ///// <param name="taskID">№ of a taskbox item, ordered by ascending</param>
        ///// <param name="rate">Rate per hours(freelancer mode availeble only)</param>
        //public TaskBox(int taskID, float rate, ProjectDescription project)
        //{
        //    InitializeComponent();
        //    textBlock.Tag = ID;
        //    this._model = new TaskModel(taskID, project);
        //    //_model.CreateDB();
        //    this.DataContext = this._model;
        //} 
        //public TaskBox(int taskID, int projectId)
        //{
        //    InitializeComponent();
        //    textBlock.Tag = ID;
        //    this._model = new TaskModel(taskID, projectId);
        //    _model.OpenDB();
        //    this.DataContext = this._model;
        //    if (_model.DBContext.TaskDataEntities.Count() > 0)
        //    {
        //        btnTimer.Content = "Resume";
        //    }
        //    else
        //    {
        //        btnTimer.Content = "Start";
        //    }
        //}

        ///// <summary>
        ///// Creating a TaskBox item with enebled Freelancer Mode and hour rate
        ///// </summary>
        ///// <param name="taskID">№ of a taskbox item, ordered by ascending</param>
        ///// <param name="logged">A raw representation of the logged time(without formatting)</param>
        ///// <param name="name">Name of task</param>
        ///// <param name="rate">Rate per hours(freelancer mode availeble only)</param>
        //public TaskBox(int taskID, int logged, string name, float rate, ProjectDescription project)
        //{
        //    InitializeComponent();
        //    textBlock.Tag = ID;
        //    this._model = new TaskModel(taskID, logged, name, project);
        //    _model.OpenDB();
        //    this.DataContext = this._model;
        //    if (_model.DBContext.TaskDataEntities.Count() > 0)
        //    {
        //        btnTimer.Content = "Resume";
        //    }
        //    else
        //    {
        //        btnTimer.Content = "Start";
        //    }
        //    _model.HourRate = rate;
           
        //}
        //public int ID { get; set; }

        ///// <summary>
        ///// Tick for a timer
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void Timer_Tick(object sender, EventArgs e)
        //{
        //    _model.Timer_Tick(sender, e);
        //}
   

        ///// <summary>
        ///// Start/pause timer
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnStartPause_Click(object sender, RoutedEventArgs e)
        //{
        //    var b = sender as Button;
        //    if (_timerIsActive == false)
        //    {
        //        if (b.Content.ToString() == "Start")
        //        {
        //            //_model.InsertTask(new TaskInfo { TaskBoxID = this.ID });
        //        }
        //        _model.StartResumeTimer();
        //        b.Content = "Pause";
        //        b.Background = Brushes.DarkGray;
        //        _timerIsActive = true;
        //    }
        //    else
        //    {
        //        _model.StopPauseTimer();
        //        textBlock.Foreground = Brushes.Blue;
        //        b.Content = "Resume";
        //        b.Background = Brushes.Gray;
        //        _timerIsActive = false;
        //    }
        }
    }
