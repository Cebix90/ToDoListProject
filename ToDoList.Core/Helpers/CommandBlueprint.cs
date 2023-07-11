/*using System.Windows.Input;

namespace ToDoList.Core.Helpers;

public class CommandBlueprint : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;
    public event EventHandler? CanExecuteChanged;

    public CommandBlueprint(Action execute) : this(execute, null)
    {
    }

    public CommandBlueprint(Action execute, Func<bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object parameter)
    {
        _execute();
    }
}*/