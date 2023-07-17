using System;
using System.Linq;
using System.Windows;
using ToDoList.Core.ViewModels;
using ToDoList.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Window
    {
        private bool isWindowOpen = false;
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

        /// <summary>
        /// Handles the event when a new work task is requested, opens the new work task page, and prevents multiple windows from opening.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorkTasksPageViewModel_NewWorkTaskRequested(object sender, System.EventArgs e)
        {
            if (isWindowOpen)
            {
                return;
            }

            var newWorkTaskPage = new NewWorkTaskPage(_workTasksPageViewModel.NewWorkTaskPageViewModel);
            newWorkTaskPage.Height = 550;
            newWorkTaskPage.Width = 400;
            newWorkTaskPage.Closed += NewWorkTaskPage_Closed;
            newWorkTaskPage.Show();

            isWindowOpen = true;
        }

        /// <summary>
        /// Handles the event when the new work task page is closed and sets the window status accordingly.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NewWorkTaskPage_Closed(object sender, EventArgs e)
        {
            isWindowOpen = false;
        }

        /// <summary>
        /// Handles the event when the logout is requested, closes the new work task page if it is open, and navigates to the login page.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void WorkTasksPageViewModel_LogoutRequested(object sender, System.EventArgs e)
        {
            if (isWindowOpen)
            {
                var newWorkTaskPage = Application.Current.Windows.OfType<NewWorkTaskPage>().FirstOrDefault();
                if (newWorkTaskPage != null)
                {
                    newWorkTaskPage.Close();
                }
            }
            
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