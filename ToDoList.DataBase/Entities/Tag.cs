namespace ToDoList.Database.Entities;

public class Tag
{
    /// <summary>
    /// Gets or sets the ID of the tag.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the value of the tag.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the list of work tasks associated with the tag.
    /// </summary>
    public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}