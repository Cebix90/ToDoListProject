using System.Windows.Input;

namespace ToDoList.Core.Helpers;

/// <summary>
/// A command implementation that relays its functionality to the provided delegates.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    /// <summary>
    /// Event that is raised when the ability of the command to execute changes.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The delegate to execute when the command is executed.</param>
    public RelayCommand(Action execute)
    : this(execute, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The delegate to execute when the command is executed.</param>
    /// <param name="canExecute">The delegate to determine if the command can execute.</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command. Unused for this implementation.</param>
    /// <returns>true if the command can execute; otherwise, false.</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">Data used by the command. Unused for this implementation.</param>
    public void Execute(object parameter)
    {
        _execute();
    }

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}