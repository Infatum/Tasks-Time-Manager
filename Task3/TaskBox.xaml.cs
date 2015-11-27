﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Task3
{
    /// <summary>
    /// Interaction logic for TaskBox.xaml
    /// </summary>
    public partial class TaskBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        DispatcherTimer _timer;
        TaskModel _model;
        //TaskViewModel p;
        bool _timerIsActive;
        string _title;

        public TaskBox(int taskID)
        {
            InitializeComponent();
            textBlock.Tag = ID;
            this._model = new TaskModel(taskID);
            this.DataContext = this._model;
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
        /// Stops the timer TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
   

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
                    _model.AddSession(_model.Time,textBoxTask.Text);
                }
                _model.StartResumeTimer();
                b.Content = "Pause";
                b.Background = Brushes.DarkGray;
                _timerIsActive = true;

                if (_model.Time % 3 == 0)
                {
                    _model.UpdateSession(_model.ModelTaskID, _model.Time, _model.Name);
                }
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
