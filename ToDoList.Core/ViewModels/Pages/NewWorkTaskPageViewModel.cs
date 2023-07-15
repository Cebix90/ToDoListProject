using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database.Entities;

namespace ToDoList.Core.ViewModels.Pages
{
    public class NewWorkTaskPageViewModel : BaseViewModel
    {
        private Guid _loggedInUserId;

        /// <summary>
        /// Gets or sets the title of the new work task.
        /// </summary>
        public string NewWorkTaskTitle { get; set; }

        /// <summary>
        /// Gets or sets the description of the new work task.
        /// </summary>
        public string NewWorkTaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the category of the new work task.
        /// </summary>
        public string NewWorkTaskCategory { get; set; }

        /// <summary>
        /// Gets or sets the status of the new work task.
        /// </summary>
        public string NewWorkTaskStatus { get; set; }

        /// <summary>
        /// Event raised when a new task is added.
        /// </summary>
        public event EventHandler<TaskAddedEventArgs> TaskAdded;

        /// <summary>
        /// Event raised when the title is missing or empty.
        /// </summary>
        public event EventHandler TitleFailed;

        /// <summary>
        /// Event raised when the description is missing or empty.
        /// </summary>
        public event EventHandler DescriptionFailed;

        /// <summary>
        /// Gets or sets the collection of category options.
        /// </summary>
        public ObservableCollection<string> CategoryOptions { get; set; }

        /// <summary>
        /// Gets or sets the collection of tag options.
        /// </summary>
        public ObservableCollection<string> TagOptions { get; set; }

        /// <summary>
        /// Command for adding a new task.
        /// </summary>
        public ICommand AddNewTaskCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewWorkTaskPageViewModel"/> class.
        /// </summary>
        /// <param name="loggedInUserId">The ID of the logged-in user.</param>
        public NewWorkTaskPageViewModel(Guid loggedInUserId)
        {
            _loggedInUserId = loggedInUserId;

            AddNewTaskCommand = new RelayCommand(AddNewTask);

            CategoryOptions = new ObservableCollection<string>(DatabaseLocator.Database.Categories.Select(c => c.Value));
            TagOptions = new ObservableCollection<string>(DatabaseLocator.Database.Tags.Select(t => t.Value));

            NewWorkTaskCategory = "Other";
            NewWorkTaskStatus = "Pending";
        }

        private void AddNewTask()
        {
            if (string.IsNullOrEmpty(NewWorkTaskTitle))
            {
                TitleFailed?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (string.IsNullOrEmpty(NewWorkTaskDescription))
            {
                DescriptionFailed?.Invoke(this, EventArgs.Empty);
                return;
            }

            var selectedCategory = DatabaseLocator.Database.Categories.FirstOrDefault(c => c.Value == NewWorkTaskCategory);
            var selectedTag = DatabaseLocator.Database.Tags.FirstOrDefault(t => t.Value == NewWorkTaskStatus);

            var newTask = new WorkTask
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                Category = selectedCategory,
                Tag = selectedTag,
                StartDate = DateTime.Now,
                UserId = _loggedInUserId
            };
            
            DatabaseLocator.Database.WorkTasks.Add(newTask);
            
            DatabaseLocator.Database.SaveChanges();
            
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            
            ClearFields();
        }

        private void ClearFields()
        {
            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;
            NewWorkTaskCategory = "Other";
            NewWorkTaskStatus = "Pending";
        }
    }

    /// <summary>
    /// Event arguments for the task added event.
    /// </summary>
    public class TaskAddedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the added task.
        /// </summary>
        public WorkTask AddedTask { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAddedEventArgs"/> class.
        /// </summary>
        /// <param name="addedTask">The added task.</param>
        public TaskAddedEventArgs(WorkTask addedTask)
        {
            AddedTask = addedTask;
        }
    }
}