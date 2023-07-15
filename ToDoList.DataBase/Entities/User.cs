namespace ToDoList.Database.Entities;

public class User
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the nickname of the user.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// Gets or sets the country of the user.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the list of work tasks associated with the user.
    /// </summary>
    public List<WorkTask> Task { get; set; } = new List<WorkTask>();

    /// <summary>
    /// Gets or sets the list of comments made by the user.
    /// </summary>
    public List<Comment> Comments { get; set; } = new List<Comment>();
}