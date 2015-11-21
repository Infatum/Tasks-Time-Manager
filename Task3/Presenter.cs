using System;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace Task3
{
    public class Presenter
    {
        Model model = null;
        MainWindow mainwindow = null;
        DispatcherTimer Timer;
        int n = 0;

        public Presenter(MainWindow mainWindow)
        {
            this.model = new Model();
            this.mainwindow = mainWindow;
        }
        internal Model Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
