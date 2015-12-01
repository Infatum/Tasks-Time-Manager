using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TaskBox> tasks;
        public TaskBox tb = null;
        private bool timerIsActive;
        static int taskCounter = 0;
        private TaskModel _model;
        //private TaskViewModel presenter = null;

        public static int TaskID { get { return taskCounter; } }

        public MainWindow()
        {
            //presenter = new TaskViewModel(this);

            timerIsActive = false;
            tasks = new ObservableCollection<TaskBox>();
            _model = new TaskModel();
            _model.LoadSession();
            InitializeComponent();
            LoadTaskSessions();

        }

        //internal TaskViewModel Presenter
        //{
        //    get { return presenter; }
        //}
        private void LoadTaskSessions()
        {
            using (TaskInfoContext db = new TaskInfoContext())
            {
                var savedSessions = db.TaskDataEntities.ToList();
                foreach (TaskInfo session in savedSessions)
                {
                    tb = new TaskBox(session.TaskBoxID);
                    tb.ID = session.TaskBoxID;
                    tasks.Add(tb);
                    tasksStackPanel.Children.Add(tb);
                    tb.textBoxTask.Text = session.Name;
                    tb.textBlock.Text = TaskModel.ShowTime(session.TrackedTime);

                }
            }           
        }

        private void btnAddTimer_Click(object sender, RoutedEventArgs e)
        {
            tb = new TaskBox(taskCounter);
            tb.ID = taskCounter;
            tasks.Add(tb);
            tasksStackPanel.Children.Add(tb);
            taskCounter++;
            
        }
    }
}
