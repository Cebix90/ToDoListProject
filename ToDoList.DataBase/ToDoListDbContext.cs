using Microsoft.EntityFrameworkCore;
using ToDoList.Database.Entities;

namespace ToDoList.Database;

/// <summary>
/// Represents the database context for the ToDoList application.
/// </summary>
public class ToDoListDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToDoListDbContext"/> class.
    /// </summary>
    public ToDoListDbContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToDoListDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the database set for the <see cref="Category"/> entity.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the database set for the <see cref="Comment"/> entity.
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    /// <summary>
    /// Gets or sets the database set for the <see cref="Tag"/> entity.
    /// </summary>
    public DbSet<Tag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the database set for the <see cref="WorkTask"/> entity.
    /// </summary>
    public DbSet<WorkTask> WorkTasks { get; set; }

    /// <summary>
    /// Gets or sets the database set for the <see cref="User"/> entity.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Configures the model by applying entity configurations from the assembly.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    /// <summary>
    /// Configures the database options and connection string for the context.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the context options.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=ToDoList.db");
    }
}