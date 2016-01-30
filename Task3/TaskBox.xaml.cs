using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;


namespace Task3
{
    /// <summary>
    /// Interaction logic for TaskBox.xaml
    /// </summary>
    public partial class TaskBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TaskModel _model;
        bool _timerIsActive;

        public TaskBox(int taskID)
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this._model = new TaskModel(taskID);
            //_model.CreateDB();
            this.DataContext = this._model;
        }

        public TaskBox(int taskID, int logged, string name)
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this._model = new TaskModel(taskID, logged, name);
            _model.CreateDB();
            this.DataContext = this._model;
            if (_model.DBContext.TaskDataEntities.Count() > 0)
            {
                btnTimer.Content = "Resume";
            }
            else
            {
                btnTimer.Content = "Start";
            }
        }

        /// <summary>
        /// Creating a TaskBox item with enebled Freelancer Mode and hour rate
        /// </summary>
        /// <param name="taskID">№ of a taskbox item, ordered by ascending</param>
        /// <param name="logged">A raw representation of the logged time(without formatting)</param>
        /// <param name="name">Name of task</param>
        /// <param name="rate">Rate per hours(freelancer mode availeble only)</param>
        public TaskBox(int taskID, int logged, string name, float rate)
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this._model = new TaskModel(taskID, logged, name);
            _model.CreateDB();
            this.DataContext = this._model;
            if (_model.DBContext.TaskDataEntities.Count() > 0)
            {
                btnTimer.Content = "Resume";
            }
            else
            {
                btnTimer.Content = "Start";
            }
            _model.HourRate = rate;
           
        }
        public int ID { get; set; }

        /// <summary>
        /// Tick for a timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            _model.Timer_Tick(sender, e);
        }
   

        /// <summary>
        /// Start/pause timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartPause_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (_timerIsActive == false)
            {
                if (b.Content.ToString() == "Start")
                {
                    _model.InsertSession(new TaskInfo { TaskBoxID = this.ID });
                }
                _model.StartResumeTimer();
                b.Content = "Pause";
                b.Background = Brushes.DarkGray;
                _timerIsActive = true;
            }
            else
            {
                _model.StopPauseTimer();
                textBlock.Foreground = Brushes.Blue;
                b.Content = "Resume";
                b.Background = Brushes.Gray;
                _timerIsActive = false;
            }
        }
    }
}
