using ToDoList.Database;

namespace ToDoList.Core;

public class DatabaseLocator
{
    /// <summary>
    /// Gets or sets the instance of the database context.
    /// </summary>
    public static ToDoListDbContext Database { get; set; }
}