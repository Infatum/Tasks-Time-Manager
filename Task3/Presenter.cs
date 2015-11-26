using System;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace Task3
{
    public class TaskViewModel 
    {
        TaskModel model = null;
        MainWindow mainwindow = null;
        DispatcherTimer Timer;
        int n = 0;

        public TaskViewModel(MainWindow mainWindow)
        {
            this.model = new TaskModel();
            this.mainwindow = mainWindow;
        }
        internal TaskModel Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
