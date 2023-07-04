using Microsoft.EntityFrameworkCore;

namespace ToDoList.DataAccess.ViewModels.Entities;

public class ToDoListContext : DbContext
{
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<WorkTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}