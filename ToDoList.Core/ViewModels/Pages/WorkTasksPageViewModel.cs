using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Core.Models.Controls;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList.Core.ViewModels;

public class WorkTasksPageViewModel : BaseViewModel
{
    public NewWorkTaskPageViewModel NewWorkTaskPageViewModel { get; private set; }

    public event EventHandler NewWorkTaskRequested;
    public event EventHandler LogoutRequested;

    public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();

    public ICommand NewWorkTaskCommand { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; set; }
    public ICommand FinishSelectedTasksCommand { get; set; }
    public ICommand LogoutCommand { get; set; }

    public WorkTasksPageViewModel()
    {
        NewWorkTaskCommand = new RelayCommand(NavigateToNewWorkTaskPage);
        DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        FinishSelectedTasksCommand = new RelayCommand(FinishSelectedTask);
        LogoutCommand = new RelayCommand(Logout);

        var newWorkTaskPageViewModel = new NewWorkTaskPageViewModel();
        newWorkTaskPageViewModel.TaskAdded += HandleTaskAdded;

        LoadTasksFromDatabase();
    }

    private void LoadTasksFromDatabase()
    {

        var tasks = DatabaseLocator.Database.WorkTasks.ToList();

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
        NewWorkTaskPageViewModel = new NewWorkTaskPageViewModel();
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

                var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == task.Id);
                if (foundEntity != null)
                {
                    foundEntity.EndDate = task.EndDate;
                    foundEntity.IsFinalized = true;
                }
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