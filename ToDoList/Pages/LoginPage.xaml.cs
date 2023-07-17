using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

            _loginPageViewModel = new LoginPageViewModel(new ToDoListDbContext());

            _loginPageViewModel.LoginSuccess += LoginPageViewModel_LoginSuccess;
            _loginPageViewModel.LoginFailed += LoginPageViewModel_LoginFailed;
            _loginPageViewModel.SignUpRequested += LoginPageViewModel_SignUpRequested;

            PasswordBox.KeyUp += PasswordBox_KeyUp;

            DataContext = _loginPageViewModel;

            Loaded += LoginPage_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the login page and subscribes to the PasswordChanged event of the password box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        /// <summary>
        /// Handles the event when the login is successful, navigates to the work task page, and closes the current login page.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LoginPageViewModel_LoginSuccess(object sender, EventArgs e)
        {
            var workTaskPage = new WorkTasksPage(_loginPageViewModel.LoggedInUserId);
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            Application.Current.MainWindow = workTaskPage;
            workTaskPage.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the event when the login fails, displays a message box with an appropriate message, and clears the email and password fields.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LoginPageViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Wrong Email or Password");
            EmailTextBox.Text = "";
            PasswordBox.Password = "";
        }

        /// <summary>
        /// Handles the PasswordChanged event of the password box and updates the password in the view model accordingly.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _loginPageViewModel.Password = passwordBox.Password;
            }
        }

        /// <summary>
        /// Handles the event when the sign-up is requested, navigates to the sign-up page, and closes the current login page.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void LoginPageViewModel_SignUpRequested(object sender, EventArgs e)
        {
            var signUpPage = new SignUpPage();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            signUpPage.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the KeyUp event of the password box and executes the login command when the Enter key is pressed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PasswordBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _loginPageViewModel.LoginCommand.Execute(null);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the text box and executes the login command when the Enter key is pressed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                _loginPageViewModel.LoginCommand.Execute(null);
            }
        }
    }
}