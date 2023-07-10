namespace ToDoList.DataBase.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Value { get; set; }
    public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}