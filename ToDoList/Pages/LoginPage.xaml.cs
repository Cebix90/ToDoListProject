﻿using System;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
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
            _loginPageViewModel.SignUpRequested += LoginPageViewModel_SignUpRequested;


            DataContext = _loginPageViewModel;

            Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
        private void LoginPageViewModel_LoginSuccess(object sender, EventArgs e)
        {
            var workTaskPage = new WorkTasksPage();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
            Application.Current.MainWindow = workTaskPage;
            workTaskPage.Show();
        }

        private void LoginPageViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Wrong Email or Password");
            EmailTextBox.Text = "";
            PasswordBox.Password = "";
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _loginPageViewModel.Password = passwordBox.Password;
            }
        }

        private void LoginPageViewModel_SignUpRequested(object sender, EventArgs e)
        {
            var signUpPage = new SignUpPage();
            signUpPage.Show();
            this.Close();
        }
    }
}