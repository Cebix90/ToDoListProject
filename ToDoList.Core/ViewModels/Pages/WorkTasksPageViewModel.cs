using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Core.Models.Controls;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList.Core.ViewModels;

/// <summary>
/// View model for the work tasks page.
/// </summary>
public class WorkTasksPageViewModel : BaseViewModel
{
    private Guid _loggedInUserId;

    /// <summary>
    /// Gets or sets the user nickname.
    /// </summary>
    public string UserNickname { get; set; }

    /// <summary>
    /// Event raised when a new work task is requested.
    /// </summary>
    public event EventHandler NewWorkTaskRequested;

    /// <summary>
    /// Event raised when a logout is requested.
    /// </summary>
    public event EventHandler LogoutRequested;

    /// <summary>
    /// Gets the view model for the new work task page.
    /// </summary>
    public NewWorkTaskPageViewModel NewWorkTaskPageViewModel { get; private set; }

    /// <summary>
    /// Gets or sets the collection of work tasks.
    /// </summary>
    public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();

    /// <summary>
    /// Gets or sets the collection of category options.
    /// </summary>
    public ObservableCollection<string> CategoryOptions { get; set; }

    /// <summary>
    /// Gets or sets the collection of tag options.
    /// </summary>
    public ObservableCollection<string> TagOptions { get; set; }

    /// <summary>
    /// Gets the command for creating a new work task.
    /// </summary>
    public ICommand NewWorkTaskCommand { get; set; }

    /// <summary>
    /// Gets the command for deleting selected tasks.
    /// </summary>
    public ICommand DeleteSelectedTasksCommand { get; set; }

    /// <summary>
    /// Gets the command for finishing selected tasks.
    /// </summary>
    public ICommand FinishSelectedTasksCommand { get; set; }

    /// <summary>
    /// Gets the command for saving changes to selected tasks.
    /// </summary>
    public ICommand SaveChangesSelectedTasksCommand { get; set; }

    /// <summary>
    /// Gets the command for logging out.
    /// </summary>
    public ICommand LogoutCommand { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkTasksPageViewModel"/> class.
    /// </summary>
    /// <param name="loggedInUserId">The ID of the logged-in user.</param>
    public WorkTasksPageViewModel(Guid loggedInUserId)
    {
        _loggedInUserId = loggedInUserId;

        UserNickname = DatabaseLocator.Database.Users.FirstOrDefault(u => u.Id == _loggedInUserId)?.NickName;

        NewWorkTaskCommand = new RelayCommand(NavigateToNewWorkTaskPage);
        DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        FinishSelectedTasksCommand = new RelayCommand(FinishSelectedTask);
        SaveChangesSelectedTasksCommand = new RelayCommand(SaveChangesSelectedTasks);
        LogoutCommand = new RelayCommand(Logout);

        CategoryOptions = new ObservableCollection<string>(DatabaseLocator.Database.Categories.Select(c => c.Value));
        TagOptions = new ObservableCollection<string>(DatabaseLocator.Database.Tags.Select(t => t.Value));

        var newWorkTaskPageViewModel = new NewWorkTaskPageViewModel(_loggedInUserId);
        newWorkTaskPageViewModel.TaskAdded += HandleTaskAdded;

        LoadTasksFromDatabase();
    }

    /// <summary>
    /// Loads tasks from the database and updates the task list.
    /// </summary>
    private void LoadTasksFromDatabase()
    {
        var tasks = DatabaseLocator.Database.WorkTasks
            .Where(t => t.UserId == _loggedInUserId)
            .ToList();

        foreach (var task in tasks)
        {
            var category = DatabaseLocator.Database.Categories.FirstOrDefault(c => c.Id == task.CategoryId);
            var tag = DatabaseLocator.Database.Tags.FirstOrDefault(t => t.Id == task.TagId);

            var viewModel = new WorkTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Category = category?.Value,
                Status = tag?.Value,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                IsFinalized = task.IsFinalized
            };

            WorkTaskList.Add(viewModel);
        }

        UpdateRowNumbers();
    }

    /// <summary>
    /// Navigates to the new work task page and initializes the view model for the page.
    /// </summary>
    private void NavigateToNewWorkTaskPage()
    {
        NewWorkTaskPageViewModel = new NewWorkTaskPageViewModel(_loggedInUserId);
        NewWorkTaskPageViewModel.TaskAdded += HandleTaskAdded;
        NewWorkTaskRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Marks the selected tasks as completed, updates their properties, and saves the changes to the database.
    /// </summary>
    private void FinishSelectedTask()
    {
        var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

        foreach (var task in selectedTasks)
        {
            if (!task.IsFinalized)
            {
                task.EndDate = DateTime.Now;
                task.IsFinalized = true;
                task.Status = "Completed";

                var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == task.Id);
                if (foundEntity != null)
                {
                    foundEntity.EndDate = task.EndDate;
                    foundEntity.IsFinalized = true;
                    foundEntity.TagId = 6;
                }
            }
        }

        DatabaseLocator.Database.SaveChanges();
    }

    /// <summary>
    /// Saves changes for the selected tasks by updating their corresponding entities in the database.
    /// </summary>
    private void SaveChangesSelectedTasks()
    {
        var selectedTasks = WorkTaskList.Where(x => x is { IsSelected: true, IsFinalized: false }).ToList();

        foreach (var task in selectedTasks)
        {
            var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == task.Id);
            if (foundEntity != null)
            {
                foundEntity.Title = task.Title;
                foundEntity.Description = task.Description;
                foundEntity.Category = DatabaseLocator.Database.Categories.FirstOrDefault(c => c.Value == task.Category);
                foundEntity.Tag = DatabaseLocator.Database.Tags.FirstOrDefault(t => t.Value == task.Status);
            }
        }

        DatabaseLocator.Database.SaveChanges();
    }

    /// <summary>
    /// Deletes the selected tasks from the task list and removes their corresponding entities from the database.
    /// </summary>
    private void DeleteSelectedTasks()
    {
        var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

        foreach (var task in selectedTasks)
        {
            WorkTaskList.Remove(task);

            var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == task.Id);
            if (foundEntity != null)
            {
                DatabaseLocator.Database.WorkTasks.Remove(foundEntity);
            }
        }

        DatabaseLocator.Database.SaveChanges();

        UpdateRowNumbers();
    }

    /// <summary>
    /// Handles the event when a task is added, and updates the task list accordingly.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments containing the added task.</param>
    private void HandleTaskAdded(object sender, TaskAddedEventArgs e)
    {
        var addedTask = e.AddedTask;

        if (addedTask.UserId == _loggedInUserId)
        {
            var viewModel = new WorkTaskViewModel
            {
                Id = addedTask.Id,
                Title = addedTask.Title,
                Description = addedTask.Description,
                Category = addedTask.Category?.Value,
                Status = addedTask.Tag?.Value,
                StartDate = addedTask.StartDate,
                EndDate = addedTask.EndDate,
                IsFinalized = addedTask.IsFinalized
            };

            WorkTaskList.Add(viewModel);

            UpdateRowNumbers();
        }
    }

    /// <summary>
    /// Updates the row numbers of the work task view models in the task list.
    /// </summary>
    private void UpdateRowNumbers()
    {
        for (int i = 0; i < WorkTaskList.Count; i++)
        {
            WorkTaskList[i].RowNumber = i + 1;
        }
    }

    /// <summary>
    /// Initiates the logout process by raising the logout event.
    /// </summary>
    private void Logout()
    {
        LogoutRequested?.Invoke(this, EventArgs.Empty);
    }
}