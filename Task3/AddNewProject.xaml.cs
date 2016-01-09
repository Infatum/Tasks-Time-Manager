using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task3
{
    /// <summary>
    /// Interaction logic for AddNewProject.xaml
    /// </summary>
    public partial class AddNewProject : Window, INotifyPropertyChanged
    {
        ProjectDescription _currentProject;
        string _name;
        string _description;
        public event PropertyChangedEventHandler PropertyChanged;
        public AddNewProject()
        {
            InitializeComponent();
            _currentProject = new ProjectDescription();
            this.DataContext = this;
        }
        public string ProjectNameText { get { return _name; } set { _name = value; } }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("ProjectNameText");
            }
        }
        public string ProjectDescriptionText { get { return _description; } set { _description = value; } }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("ProjectDescriptionText");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // Check if no interface? do nothing
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //_currentProject = new ProjectDescription { ProjectName = Name,  }
        }
    }
}
