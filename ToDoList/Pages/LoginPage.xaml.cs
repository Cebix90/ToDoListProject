using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToDoList.Core.ViewModels.Pages;
using ToDoList.Database;

namespace ToDoList.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private readonly LoginPageViewModel _loginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();

            _loginPageViewModel = new LoginPageViewModel(new ToDoListDbContext());

            _loginPageViewModel.LoginSuccess += LoginPageViewModel_LoginSuccess;
            _loginPageViewModel.LoginFailed += LoginPageViewModel_LoginFailed;

            DataContext = _loginPageViewModel;

            Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _loginPageViewModel.Password = passwordBox.Password;
            }
        }
    }
}