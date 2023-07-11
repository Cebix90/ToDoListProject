using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Core.Models.Controls;
using ToDoList.Database.Entities;

namespace ToDoList.Core.Models;

public class WorkTasksPageViewModel : BaseViewModel
{
    public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();
    public string NewWorkTaskTitle { get; set; }
    public string NewWorkTaskDescription { get; set; }

    public ICommand AddNewTaskCommand { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; set; }
    public ICommand FinishSelectedTasksCommand { get; set; }

    public WorkTasksPageViewModel()
    {
        AddNewTaskCommand = new RelayCommand(AddNewTask);
        DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        FinishSelectedTasksCommand = new RelayCommand(FinishSelectedTask);

        LoadTasksFromDatabase();
    }

    private void LoadTasksFromDatabase()
    {
        var tasks = DatabaseLocator.Database.WorkTasks.ToList();

        foreach (var task in tasks)
        {
            var viewModel = new WorkTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                IsFinalized = task.IsFinalized
            };

            WorkTaskList.Add(viewModel);
        }
    }

    private void AddNewTask()
    {
        var newTask = new WorkTaskViewModel
        {
            Title = NewWorkTaskTitle,
            Description = NewWorkTaskDescription,
            StartDate = DateTime.Now
        };
        
        WorkTaskList.Add(newTask);

        DatabaseLocator.Database.WorkTasks.Add(new WorkTask
        {
            Id = newTask.Id,
            Title = newTask.Title,
            Description = newTask.Description,
            StartDate = newTask.StartDate
        });

        DatabaseLocator.Database.SaveChanges();

        var refreshedTasks = DatabaseLocator.Database.WorkTasks.ToList();
        WorkTaskList.Clear();
        foreach (var task in refreshedTasks)
        {
            var viewModel = new WorkTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate
            };

            WorkTaskList.Add(viewModel);
        }

        NewWorkTaskTitle = string.Empty;
        NewWorkTaskDescription = string.Empty;
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
    }
}