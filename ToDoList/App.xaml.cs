using System.Windows;
using ToDoList.Core;
using ToDoList.Database;
using ToDoList.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginPage loginPage = new LoginPage();
            loginPage.Show();

            var database = new ToDoListDbContext();

            database.Database.EnsureCreated();

            DatabaseLocator.Database = database;
            
            new Seeds(database).SeedData();
        }
    }
}
