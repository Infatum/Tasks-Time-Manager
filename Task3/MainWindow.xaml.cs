using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Task3
{
    // TODO: http://www.c-sharpcorner.com/UploadFile/013102/how-to-create-report-rdlc-in-wpf/
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TaskBox> tasks;
        public TaskBox tb = null;
        static int taskCounter = 0;
        TaskInfoContext _db;

        public static int TaskID { get { return taskCounter; } }

        public MainWindow()
        {
            tasks = new ObservableCollection<TaskBox>();
            InitializeComponent();
            LoadTaskSessions();
            _db = new TaskInfoContext();
            taskCounter = getMaxTaskID() + 1;
        }

        private int getMaxTaskID()
        {
            var sql = "SELECT MAX(TaskBoxID) FROM TASKINFOES;";
            return _db.Database.SqlQuery<int>(sql).FirstOrDefault<int>();
        }

        private void LoadTaskSessions()
        {
            using (TaskInfoContext db = new TaskInfoContext())
            {
                var savedSessions = db.TaskDataEntities.ToList();
                foreach (TaskInfo session in savedSessions)
                {
                    tb = new TaskBox(session.TaskBoxID, session.TrackedTime, session.Name);
                    tasks.Add(tb);
                    tasksStackPanel.Children.Add(tb);
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
