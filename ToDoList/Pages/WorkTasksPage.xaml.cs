using System.Windows;
using ToDoList.Core.Models;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Window
    {
        public WorkTasksPage()
        {
            InitializeComponent();

            DataContext = new WorkTasksPageViewModel();
        }
    }
}
