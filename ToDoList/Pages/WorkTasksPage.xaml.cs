using System.Windows;
using ToDoList.Core.ViewModels;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Window
    {
        private readonly WorkTasksPageViewModel _workTasksPageViewModel;
        public WorkTasksPage()
        {
            InitializeComponent();

            _workTasksPageViewModel = new WorkTasksPageViewModel();

            _workTasksPageViewModel.NewWorkTaskRequested += WorkTasksPageViewModel_NewWorkTaskRequested;

            DataContext = _workTasksPageViewModel;
        }

        /*private void WorkTasksPageViewModel_NewWorkTaskRequested(object sender, System.EventArgs e)
        {
            var newWorkTaskPage = new NewWorkTaskPage();
            newWorkTaskPage.Height = 450;
            newWorkTaskPage.Width = 300;
            newWorkTaskPage.Show();
        }*/

        private void WorkTasksPageViewModel_NewWorkTaskRequested(object sender, System.EventArgs e)
        {
            var newWorkTaskPage = new NewWorkTaskPage(_workTasksPageViewModel.NewWorkTaskPageViewModel);
            newWorkTaskPage.Height = 450;
            newWorkTaskPage.Width = 300;
            newWorkTaskPage.Show();
        }

    }
}
