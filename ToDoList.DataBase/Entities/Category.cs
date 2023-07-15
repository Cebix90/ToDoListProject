namespace ToDoList.Database.Entities;

public class Category
{
    /// <summary>
    /// Gets or sets the ID of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the value of the category.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the list of work tasks associated with the category.
    /// </summary>
    public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}