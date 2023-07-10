using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.DataAccess;
using ToDoList.DataAccess.ViewModels.Entities;

namespace ToDoList.Core.ViewModels.Pages;

public class WorkTasksPageViewModel : BaseViewModel
{
    public ObservableCollection<WorkTask> WorkTaskList { get; set; } = new ObservableCollection<WorkTask>();
    public string NewWorkTaskTitle { get; set; }
    public string NewWorkTaskDescription { get; set; }

    public ICommand AddNewTaskCommand { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; set; }

    public WorkTasksPageViewModel()
    {
        AddNewTaskCommand = new RelayCommand(AddNewTask);
        DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
    }

    

    private void AddNewTask()
    {
        var newTask = new WorkTask
        {
            Title = NewWorkTaskTitle,
            Description = NewWorkTaskDescription,
            StartDate = DateTime.Now
        };
        
        
        WorkTaskList.Add(newTask);
        
        NewWorkTaskTitle = string.Empty;
        NewWorkTaskDescription = string.Empty;

        //OnPropertyChanged(nameof(NewWorkTaskTitle));
        //OnPropertyChanged(nameof(NewWorkTaskDescription));
    }

    private void AddWorkTaskEndDate()
    {
        var lastTask = WorkTaskList.LastOrDefault();
        if (lastTask != null)
        {
            lastTask.EndDate = DateTime.Now;
        }
    }

    private void DeleteSelectedTasks()
    {
        var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

        foreach (var task in selectedTasks)
        {
            WorkTaskList.Remove(task);
        }
    }
}