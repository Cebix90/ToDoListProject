using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Core.Models.Controls;
using ToDoList.Core.ViewModels;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Window
    {
        private readonly WorkTasksPageViewModel _workTasksPageViewModel;
        public WorkTasksPage()
        {
            InitializeComponent();

            _workTasksPageViewModel = new WorkTasksPageViewModel();

            _workTasksPageViewModel.NewWorkTaskRequested += WorkTasksPageViewModel_NewWorkTaskRequested;

            DataContext = _workTasksPageViewModel;
        }

        private void WorkTasksPageViewModel_NewWorkTaskRequested(object sender, System.EventArgs e)
        {
            var newWorkTaskPage = new NewWorkTaskPage(_workTasksPageViewModel.NewWorkTaskPageViewModel);
            newWorkTaskPage.Height = 450;
            newWorkTaskPage.Width = 300;
            newWorkTaskPage.Show();
        }

        /*private void DataGridRow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is WorkTaskViewModel viewModel)
            {
                viewModel.IsSelected = !viewModel.IsSelected;
            }
        }*/
    }
}
