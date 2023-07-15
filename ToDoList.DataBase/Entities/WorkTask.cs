using System.ComponentModel.DataAnnotations;

namespace ToDoList.Database.Entities;

public class WorkTask
{
    /// <summary>
    /// Gets or sets the ID of the work task.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the work task.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the work task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the start date of the work task.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the work task.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the work task is finalized.
    /// </summary>
    public bool IsFinalized { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the work task.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user associated with the work task.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of comments associated with the work task.
    /// </summary>
    public List<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Gets or sets the tag associated with the work task.
    /// </summary>
    public Tag Tag { get; set; }

    /// <summary>
    /// Gets or sets the ID of the tag associated with the work task.
    /// </summary>
    public int TagId { get; set; }

    /// <summary>
    /// Gets or sets the category associated with the work task.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the ID of the category associated with the work task.
    /// </summary>
    public int CategoryId { get; set; }
}
