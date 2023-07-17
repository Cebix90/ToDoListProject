using System;
using System.Windows;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Window
    {
        private readonly SignUpPageViewModel _signUpPageViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage"/> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();

            _signUpPageViewModel = new SignUpPageViewModel();
            _signUpPageViewModel.LoginRequested += SignUpPageViewModel_LoginRequested;
            _signUpPageViewModel.EmailIsTaken += SignUpPageViewModel_EmailIsTaken;
            _signUpPageViewModel.NickNameIsTaken += SignUpPageViewModel_NickNameIsTaken;
            _signUpPageViewModel.NickNameMustBeEntered += SignUpPageViewModel_NickNameMustBeEntered;
            _signUpPageViewModel.PasswordIsToShort += SignUpPageViewModel_PasswordIsToShort;
            _signUpPageViewModel.EmailIsNotInCorrectForm += SignUpPageViewModel_EmailIsNotInCorrectForm;
            _signUpPageViewModel.SubmitSuccessfully += SignUpPageViewModel_SubmitSuccessfully;

            DataContext = _signUpPageViewModel;
        }

        /// <summary>
        /// Handles the event when the login is requested from the sign-up page, navigates to the login page, and closes the current sign-up page.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_LoginRequested(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            loginPage.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the event when the email is already taken, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_EmailIsTaken(object sender, EventArgs e)
        {
            MessageBox.Show("Email is already taken.");
        }

        /// <summary>
        /// Handles the event when the nickname is already taken, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_NickNameIsTaken(object sender, EventArgs e)
        {
            MessageBox.Show("Nickname is already taken.");
        }

        /// <summary>
        /// Handles the event when the password is too short, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_PasswordIsToShort(object sender, EventArgs e)
        {
            MessageBox.Show("Password must be at least 5 characters long.");
        }

        /// <summary>
        /// Handles the event when the email is not in the correct form, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_EmailIsNotInCorrectForm(object sender, EventArgs e)
        {
            MessageBox.Show("Email is not in a valid format.");
        }

        /// <summary>
        /// Handles the event when the nickname must be entered, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_NickNameMustBeEntered(object sender, EventArgs e)
        {
            MessageBox.Show("Nickname must be entered.");
        }

        /// <summary>
        /// Handles the event when the sign-up submission is successful, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SignUpPageViewModel_SubmitSuccessfully(object sender, EventArgs e)
        {
            MessageBox.Show("You have successfully signed up.");
        }
    }
}
