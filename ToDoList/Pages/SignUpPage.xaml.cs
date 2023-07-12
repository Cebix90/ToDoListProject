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

            DataContext = _signUpPageViewModel;
        }

        private void SignUpPageViewModel_LoginRequested(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        private void SignUpPageViewModel_EmailIsTaken(object sender, EventArgs e)
        {
            MessageBox.Show("Email is already taken.");
        }

        private void SignUpPageViewModel_NickNameIsTaken(object sender, EventArgs e)
        {
            MessageBox.Show("Nickname is already taken.");
        }

        private void SignUpPageViewModel_PasswordIsToShort(object sender, EventArgs e)
        {
            MessageBox.Show("Password must be at least 5 characters long.");
        }

        private void SignUpPageViewModel_EmailIsNotInCorrectForm(object sender, EventArgs e)
        {
            MessageBox.Show("Email is not in a valid format.");
        }

        private void SignUpPageViewModel_NickNameMustBeEntered(object sender, EventArgs e)
        {
            MessageBox.Show("Nickname must be entered.");
        }
    }

}
