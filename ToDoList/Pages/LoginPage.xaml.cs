using System;
using System.Windows;
using ToDoList.Core.ViewModels.Pages;
using ToDoList.Database;

namespace ToDoList.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private LoginPageViewModel loginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();
            
            loginPageViewModel = new LoginPageViewModel(new ToDoListDbContext());
            
            loginPageViewModel.LoginSuccess += LoginPageViewModel_LoginSuccess;
            loginPageViewModel.LoginFailed += LoginPageViewModel_LoginFailed;
            
            DataContext = loginPageViewModel;
        }

        private void LoginPageViewModel_LoginSuccess(object sender, EventArgs e)
        {
            var window = new WorkTasksPage();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            Application.Current.MainWindow = window;
            window.Show();
        }

        private void LoginPageViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login Failed");
        }
    }
}
