using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Core.Models.Controls;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList.Core.ViewModels;

public class WorkTasksPageViewModel : BaseViewModel
{
    private Guid _loggedInUserId;
    public NewWorkTaskPageViewModel NewWorkTaskPageViewModel { get; private set; }

    public event EventHandler NewWorkTaskRequested;
    public event EventHandler LogoutRequested;

    public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();

    public ObservableCollection<string> CategoryOptions { get; set; }
    public ObservableCollection<string> TagOptions { get; set; }

    public ICommand NewWorkTaskCommand { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; set; }
    public ICommand FinishSelectedTasksCommand { get; set; }
    public ICommand SaveChangesSelectedTasksCommand { get; set; }
    public ICommand LogoutCommand { get; set; }

    public WorkTasksPageViewModel(Guid loggedInUserId)
    {
        _loggedInUserId = loggedInUserId;

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

    private void NavigateToNewWorkTaskPage()
    {
        NewWorkTaskPageViewModel = new NewWorkTaskPageViewModel(_loggedInUserId);
        NewWorkTaskPageViewModel.TaskAdded += HandleTaskAdded;
        NewWorkTaskRequested?.Invoke(this, EventArgs.Empty);
    }

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

    private void SaveChangesSelectedTasks()
    {
        var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

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

    private void UpdateRowNumbers()
    {
        for (int i = 0; i < WorkTaskList.Count; i++)
        {
            WorkTaskList[i].RowNumber = i + 1;
        }
    }

    private void Logout()
    {
        LogoutRequested?.Invoke(this, EventArgs.Empty);
    }
}