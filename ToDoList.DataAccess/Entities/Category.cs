namespace ToDoList.Entities;

public class Category
{
    public int Id { get; set; }
    public string Value { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
}