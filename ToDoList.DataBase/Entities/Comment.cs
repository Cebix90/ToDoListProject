namespace ToDoList.Database.Entities;

public class Comment
{
    /// <summary>
    /// Gets or sets the ID of the comment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the text of the comment.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the author of the comment.
    /// </summary>
    public virtual User Author { get; set; }

    /// <summary>
    /// Gets or sets the ID of the author.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the created date of the comment.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date of the comment.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the associated work task of the comment.
    /// </summary>
    public WorkTask Task { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated work task.
    /// </summary>
    public int TaskId { get; set; }
}