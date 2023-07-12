namespace ToDoList.Core.Helpers;

public static class Mediator
{
    public static event EventHandler<EventArgs> NavigateToSignUpRequested;

    public static void OnNavigateToSignUpRequested()
    {
        NavigateToSignUpRequested?.Invoke(null, EventArgs.Empty);
    }
}