using System;
using System.Collections.Generic;
using System.Windows;
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
        private int taskCounter = 0;
        private TaskViewModel presenter = null;

        public int TaskID { get { return taskCounter; } }

        public MainWindow()
        {
            presenter = new TaskViewModel(this);
            timerIsActive = false;
            tasks = new ObservableCollection<TaskBox>();
            InitializeComponent();

        }

        internal TaskViewModel Presenter
        {
            get { return presenter; }
        }

        private void btnAddTimer_Click(object sender, RoutedEventArgs e)
        {
            tb = new TaskBox();
            tb.ID = taskCounter;
            tasks.Add(tb);
            tasksStackPanel.Children.Add(tb);
            taskCounter++;
            
        }
    }
}
