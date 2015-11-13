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

        /// <summary>
        /// Start/pause timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartPause_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (timerIsActive == false)
            {
                startTimerHandler.Invoke(sender, e);
                b.Content = "Pause";
                b.Background = Brushes.DarkGray;
                timerIsActive = true;
            }
            else if (timerIsActive == true)
            {
                pauseTimerHandler.Invoke(sender, e);
                b.Content = "Start";
                b.Background = Brushes.Gray;
                timerIsActive = false;
            }
        }

        private void btnStartPause_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public object lol = null;

        private void btnAddTimer_Click(object sender, RoutedEventArgs e)
        {
            tb = new TaskBox();
            tb.N = tasks;
            tb.btnTimer.Click += btnStartPause_Click;
            listTasks.Items.Add(tb);
            tasks++;

            //XElement root = XElement.Load("TaskBox.xaml");
            //var butt = from el in root.Elements("Grid") where el.Element("Button").Name == "btnAddTimer" select el;

            //listTasks.Children.Add(timerBoxUI);

            //Grid g = new Grid();
            //g.Width = 308;
            //g.Height = 193;
            //g.VerticalAlignment = VerticalAlignment.Bottom;

            // g.Margin = new Thickness();
        }
    }
}
