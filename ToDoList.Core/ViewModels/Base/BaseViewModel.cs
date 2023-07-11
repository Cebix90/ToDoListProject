using System.ComponentModel;
using ToDoList.Database;

namespace ToDoList.Core.Models.Base;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool AuthenticateUser(string email, string password, ToDoListDbContext context)
    {
        bool isAuthenticated = context.Users.Any(u => u.Email == email && u.Password == password);
        return isAuthenticated;
    }


}