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

        /// <summary>
        /// Initializes a new instance of the <see cref="NewWorkTaskPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model for the new work task page.</param>
        public NewWorkTaskPage(NewWorkTaskPageViewModel viewModel)
        {
            InitializeComponent();

            _newWorkTaskPageViewModel = viewModel;

            _newWorkTaskPageViewModel.TitleFailed += NewWorkTaskPageViewModel_TitleFailed;
            _newWorkTaskPageViewModel.DescriptionFailed += NewWorkTaskPageViewModel_DescriptionFailed;
            DataContext = _newWorkTaskPageViewModel;
        }

        /// <summary>
        /// Handles the event when the title field is empty, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NewWorkTaskPageViewModel_TitleFailed(object sender, EventArgs e)
        {
            MessageBox.Show("You have to fulfill title box");
        }

        /// <summary>
        /// Handles the event when the description field is empty, and displays a message box with an appropriate message.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void NewWorkTaskPageViewModel_DescriptionFailed(object sender, EventArgs e)
        {
            MessageBox.Show("You have to fulfill description box");
        }

        /// <summary>
        /// Handles the KeyDown event of the text box and executes the command to add a new task when the Enter or Return key is pressed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                _newWorkTaskPageViewModel.AddNewTaskCommand.Execute(null);
            }
        }
    }
}
