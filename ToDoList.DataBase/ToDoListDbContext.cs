using Microsoft.EntityFrameworkCore;
using ToDoList.Database.Entities;

namespace ToDoList.Database;

public class ToDoListDbContext : DbContext
{
    public ToDoListDbContext() { }

    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<WorkTask> WorkTasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=ToDoList.db");
    }
}