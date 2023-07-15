using System;
using System.Windows;
using System.Windows.Input;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for NewWorkTaskPage.xaml
    /// </summary>
    public partial class NewWorkTaskPage : Window
    {
        private readonly NewWorkTaskPageViewModel _newWorkTaskPageViewModel;
        public NewWorkTaskPage(NewWorkTaskPageViewModel viewModel)
        {
            InitializeComponent();

            _newWorkTaskPageViewModel = viewModel;

            _newWorkTaskPageViewModel.TitleFailed += NewWorkTaskPageViewModel_TitleFailed;
            _newWorkTaskPageViewModel.DescriptionFailed += NewWorkTaskPageViewModel_DescriptionFailed;
            DataContext = _newWorkTaskPageViewModel;
        }

        private void NewWorkTaskPageViewModel_TitleFailed(object sender, EventArgs e)
        {
            MessageBox.Show("You have to fulfill title box");
        }

        private void NewWorkTaskPageViewModel_DescriptionFailed(object sender, EventArgs e)
        {
            MessageBox.Show("You have to fulfill description box");
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                _newWorkTaskPageViewModel.AddNewTaskCommand.Execute(null);
            }
        }
    }
}
