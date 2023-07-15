using System;
using System.Windows;
using ToDoList.Core.ViewModels;
using ToDoList.Core.ViewModels.Pages;
using ToDoList.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Window
    {
        private Guid _loggedInUserId;
        private readonly WorkTasksPageViewModel _workTasksPageViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTasksPage"/> class.
        /// </summary>
        /// <param name="loggedInUserId">The ID of the logged-in user.</param>
        public WorkTasksPage(Guid loggedInUserId)
        {
            InitializeComponent();
            _loggedInUserId = loggedInUserId;
            _workTasksPageViewModel = new WorkTasksPageViewModel(_loggedInUserId);

            _workTasksPageViewModel.NewWorkTaskRequested += WorkTasksPageViewModel_NewWorkTaskRequested;
            _workTasksPageViewModel.LogoutRequested += WorkTasksPageViewModel_LogoutRequested;

            DataContext = _workTasksPageViewModel;
        }

        private void WorkTasksPageViewModel_NewWorkTaskRequested(object sender, System.EventArgs e)
        {
            var newWorkTaskPageViewModel = new NewWorkTaskPageViewModel(_loggedInUserId);
            var newWorkTaskPage = new NewWorkTaskPage(_workTasksPageViewModel.NewWorkTaskPageViewModel);
            newWorkTaskPage.Height = 450;
            newWorkTaskPage.Width = 300;
            newWorkTaskPage.Show();
        }

        private void WorkTasksPageViewModel_LogoutRequested(object sender, System.EventArgs e)
        {
            var loginPage = new LoginPage();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            Application.Current.MainWindow = loginPage;
            loginPage.Show();
            this.Close();
        }
    }
}