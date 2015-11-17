using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Media;
using System.Xml;

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
        public TaskBox tb = null;
        private bool timerIsActive;
        private int tasks = 0;
        private Presenter presenter = null;

        public int TaskID { get { return tasks; } }
        public MainWindow()
        {
            presenter = new Presenter(this);
            timerIsActive = false;
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

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            pauseTimerHandler.Invoke(sender, e);
        }

        private void btnStartPause_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public object lol = null;

        private void btnAddTimer_Click(object sender, RoutedEventArgs e)
        {
            tb = new TaskBox();
            tb.ID = tasks;
            listTasks.Items.Add(tb);
            tasks++;
        }
    }
}
