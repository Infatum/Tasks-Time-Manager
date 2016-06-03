using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Task3.ViewModel;

namespace TasksTracker
{
    /// <summary>
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        private ProjectViewModel _projectsModel;
        public Projects()
        {
            _projectsModel = new ProjectViewModel();
            InitializeComponent();
            this.listViewProjectsNamesAndDescriptions.ItemsSource = _projectsModel.LoadProjects();
            this.DataContext = _projectsModel;
        }
        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        //TODO!
        //void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var item = ((FrameworkElement)e.OriginalSource).DataContext as ProjectDescription;

        //    // Table Border (Header) clicked
        //    if (item == null) return;

        //    foreach (var p in ListOfProjects)
        //    {
        //        if (p.ProjectId == item.ProjectId)
        //            item = p;
        //    }

        //    // No project
        //    if (item == null) return;

        //    _taskWindow = new ProjectTaskWindow(item);
        //    _taskWindow.Show();

        //    // TODO: Show only one project
        //    //this.Hide();
        //}

        private void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            _projectsModel.AddProject();
            AddNewProject newProjectWindow = new AddNewProject();
            newProjectWindow.Show();
        }
    }
}
