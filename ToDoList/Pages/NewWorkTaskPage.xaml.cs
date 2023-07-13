using System.Windows;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for NewWorkTaskPage.xaml
    /// </summary>
    public partial class NewWorkTaskPage : Window
    {

        public NewWorkTaskPage()
        {
            InitializeComponent();

            DataContext = new NewWorkTaskPageViewModel();
        }
    }
}
