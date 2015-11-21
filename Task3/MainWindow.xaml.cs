using System;
using System.Windows;
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
        static EventHandler startTimerHandler = null;
        static EventHandler pauseTimerHandler = null;
        static EventHandler stopTimerHandler = null;
        ObservableCollection<TaskBox> tasks;
        public TaskBox tb = null;
        private bool timerIsActive;
        private int taskCounter = 0;
        private Presenter presenter = null;

        public int TaskID { get { return taskCounter; } }
        public MainWindow()
        {
            presenter = new Presenter(this);
            timerIsActive = false;
            tasks = new ObservableCollection<TaskBox>();
            InitializeComponent();
        }
        internal Presenter Presenter
        {
            get { return presenter; }
        }

        public event EventHandler StartTimer
        {
            add { startTimerHandler += value; }
            remove { startTimerHandler -= value; }
        }

        public event EventHandler PauseTimer
        {
            add
            {
                pauseTimerHandler += value;
            }
            remove { pauseTimerHandler -= value; }
        }

        public event EventHandler StopTimer
        {
            add { stopTimerHandler += value; }
            remove { stopTimerHandler -= value; }
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
