﻿using System;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Controls;

namespace Task3
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProjectTasks : Window
    {
        ObservableCollection<TaskBox> _tasks;
        public TaskBox tb = null;
        private TaskModel _task;
        static int taskCounter = 0;
        ProjectInfoContext _db;
        ProjectDescription _currentProject;
        float _rate;
        bool _freelancerMode;

        public event PropertyChangedEventHandler PropertyChanged;

        public static int TaskID { get { return taskCounter; } }

        public ProjectTasks(ProjectDescription project)
        {
            
            _tasks = new ObservableCollection<TaskBox>();
            InitializeComponent();
            _db = new ProjectInfoContext();
            taskCounter = getMaxTaskID() + 1;
            _currentProject = project;
            _task = new TaskModel(taskCounter, _currentProject);
            _freelancerMode = false;
            _task.CreateDB();
            LoadTaskSessions();

        }
        public ProjectTasks(string name)
        {
            _tasks = new ObservableCollection<TaskBox>();
            InitializeComponent();
            _db = new ProjectInfoContext();
            taskCounter = getMaxTaskID() + 1;
            _freelancerMode = false;
            _task = new TaskModel(taskCounter, _currentProject);
            _task.CreateDB();
            LoadTaskSessions();

        }

        private int getMaxTaskID()
        {
            try
            {
                return _db.Database.SqlQuery<int>("SELECT MAX(TaskBoxID) FROM TASKINFOES;").FirstOrDefault<int>();
            }
            catch (System.InvalidOperationException e)
            {
                return 0;
            }
        }

        private void LoadTaskSessions()
        {
            using (ProjectInfoContext db = new ProjectInfoContext())
            {
                var savedSessions = db.TaskDataEntities.ToList();
                foreach (TaskInfo session in savedSessions)
                {
                    tb = new TaskBox(session.TaskBoxID, session.TrackedTime,  session.Name, _rate, _currentProject);
                    //TODO FreelancerMode 
                    tb.textBlockHorRate.Text = _rate.ToString();
                    _tasks.Add(tb);
                    tasksStackPanel.Children.Add(tb);
                }
            }
        }

        private void btnAddTimer_Click(object sender, RoutedEventArgs e)
        {

            if (_freelancerMode == true)
            {
                _task = new TaskModel(taskCounter, _rate, _currentProject);
                _task.InsertSession(new TaskInfo() { TaskBoxID = taskCounter, Task_Id = taskCounter, HourRate = _rate, Project = _currentProject });
                tb = new TaskBox(taskCounter, _rate, _currentProject);

            }
            else
            {
                _task = new TaskModel(taskCounter, _currentProject);
                _task.InsertSession(new TaskInfo() { Task_Id = taskCounter, TaskBoxID = taskCounter, Project = _currentProject });
                tb = new TaskBox(taskCounter, _currentProject);
            }
           
            tb.ID = taskCounter;
            _tasks.Add(tb);
            tasksStackPanel.Children.Add(tb);
            taskCounter++;
        }

        private void btnAddGenerateTextReport_Click(object sender, RoutedEventArgs e)
        {
            SavePDFDocument(SaveFileDialog());
        }

        /// <summary>
        /// Saves pdf report in a choosen directory on users pc
        /// </summary>
        /// <returns>PDF file name</returns>
        public string SaveFileDialog()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Report"; //Default file name
            dlg.DefaultExt = ".pdf"; //Default file extansion
            dlg.Filter = "PDF documents (.pdf)|.*pdf"; //Filter files by extansion
            string filename = "Report";
            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }
            return filename;
        }

        /// <summary>
        /// Generating a pdf format in a table maner
        /// </summary>
        /// <param name="name"></param>
        public void SavePDFDocument(string name)
        {
            FileStream fs = new FileStream(name, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            int taskCount = 1;


            document.AddAuthor("Ann Vityck");
            document.AddCreator("Generated by Pomodoro Timer");
            //document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("Time Report");

            //PdfTable

            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 400f;
            table.LockedWidth = true; //fix the absolute width of the table

            float[] widths = new float[] { 2f, 4f, 6f }; //relative col widths in prportions - 1/3 and 2/3
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f; //leave a gap before and after the table
            table.SpacingAfter = 30f;

            PdfPCell cell = new PdfPCell(new Phrase("Tasks"));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            table.AddCell("#");
            table.AddCell("Title");
            table.AddCell("Time");

            foreach (var taskInfo in _db.TaskDataEntities)
            {
                string trakedTime = $"{ taskInfo.TrackedTime / 3600 }h { taskInfo.TrackedTime % 3600 / 60}m { taskInfo.TrackedTime % 60}s";
                table.AddCell(taskCount.ToString());
                table.AddCell(taskInfo.Name);
                table.AddCell(trakedTime);
                taskCount++;
            }

            document.Open();
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();

        }

        private void FrellancerModeOn(object sender, RoutedEventArgs e)
        {
            _freelancerMode = true;
            tbProjectRate.IsEnabled = true;
        }

        private void RateChanged(object sender, TextChangedEventArgs e)
        {
            _rate = float.Parse(tbProjectRate.Text);
        }
    }
}
