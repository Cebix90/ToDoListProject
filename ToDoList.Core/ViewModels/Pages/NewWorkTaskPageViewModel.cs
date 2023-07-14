﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database.Entities;

namespace ToDoList.Core.ViewModels.Pages
{
    public class NewWorkTaskPageViewModel : BaseViewModel
    {
        public string NewWorkTaskTitle { get; set; }
        public string NewWorkTaskDescription { get; set; }
        public string NewWorkTaskCategory { get; set; }
        public string NewWorkTaskStatus { get; set; }

        public event EventHandler<TaskAddedEventArgs> TaskAdded;

        public ObservableCollection<string> CategoryOptions { get; set; }
        public ObservableCollection<string> TagOptions { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public NewWorkTaskPageViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);

            CategoryOptions = new ObservableCollection<string>(DatabaseLocator.Database.Categories.Select(c => c.Value));
            TagOptions = new ObservableCollection<string>(DatabaseLocator.Database.Tags.Select(t => t.Value));

        }

        private void AddNewTask()
        {
            var selectedCategory = DatabaseLocator.Database.Categories.FirstOrDefault(c => c.Value == NewWorkTaskCategory);
            var selectedTag = DatabaseLocator.Database.Tags.FirstOrDefault(t => t.Value == NewWorkTaskStatus);

            var newTask = new WorkTask
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                Category = selectedCategory,
                Tag = selectedTag,
                StartDate = DateTime.Now
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
            NewWorkTaskCategory = string.Empty;
            NewWorkTaskStatus = string.Empty;
        }
    }

    public class TaskAddedEventArgs : EventArgs
    {
        public WorkTask AddedTask { get; }

        public TaskAddedEventArgs(WorkTask addedTask)
        {
            AddedTask = addedTask;
        }
    }
}