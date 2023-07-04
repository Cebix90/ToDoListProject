using System.ComponentModel;

namespace ToDoList.DataAccess;

public class BaseViewModel :INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (s , e) => { };

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}