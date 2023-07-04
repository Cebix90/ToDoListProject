namespace ToDoList.DataAccess.ViewModels.Entities;

public class Category
{
    public int Id { get; set; }
    public string Value { get; set; }
    public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}