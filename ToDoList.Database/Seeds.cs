using ToDoList.Database.Entities;
using BC = BCrypt.Net.BCrypt;

namespace ToDoList.Database;

public class Seeds
{
    private readonly ToDoListDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Seeds"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Seeds(ToDoListDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Seeds the initial data into the database.
    /// </summary>
    public void SeedData()
    {
        if (!_context.Users.Any())
        {
            SeedUsers();
        }

        if (!_context.Categories.Any())
        {
            SeedCategories();
        }

        if (!_context.Tags.Any())
        {
            SeedTags();
        }

        if (!_context.WorkTasks.Any())
        {
            SeedTasks();
        }
    }

    /// <summary>
    /// Seeds the database with sample user data.
    /// </summary>
    private void SeedUsers()
    {
        List<User> users = new List<User>
        {
            new User { Id = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), Email = "jk@example.com", Password = BC.HashPassword("password1"), NickName = "User1", Country = "Poland" },
            new User { Id = Guid.Parse("2e5c7dbf-82e1-45b0-9be0-8e7ba8e57b11"), Email = "an@example.com", Password = BC.HashPassword("password2"), NickName = "User2", Country = "Poland" },
            new User { Id = Guid.Parse("f5d3c88f-902b-4d8f-85e5-41bcf5d2cbb1"), Email = "mk@example.com", Password = BC.HashPassword("password3"), NickName = "User3", Country = "Poland" },
            new User { Id = Guid.Parse("94a860d3-28d6-4fe6-ae0d-ebf47d799c51"), Email = "ka@example.com", Password = BC.HashPassword("password4"), NickName = "User4", Country = "Poland" },
            new User { Id = Guid.Parse("1f73db4e-73e3-4397-b960-2c68b60792d4"), Email = "aj@example.com", Password = BC.HashPassword("password5"), NickName = "User5", Country = "Poland" }
        };

        _context.Users.AddRange(users);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seeds the database with sample category data.
    /// </summary>
    private void SeedCategories()
    {
        List<Category> categories = new List<Category>
        {
            new Category { Value = "Work" },
            new Category { Value = "Home" },
            new Category { Value = "School" },
            new Category { Value = "Shopping" },
            new Category { Value = "Other" }
        };

        _context.Categories.AddRange(categories);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seeds the database with sample tag data.
    /// </summary>
    private void SeedTags()
    {
        List<Tag> tags = new List<Tag>
        {
            new Tag { Value = "High priority" },
            new Tag { Value = "Medium priority" },
            new Tag { Value = "Low priority" },
            new Tag { Value = "Urgent" },
            new Tag { Value = "Pending" },
            new Tag { Value = "Completed" }
        };

        _context.Tags.AddRange(tags);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seeds the database with sample task data.
    /// </summary>
    private void SeedTasks()
    {
        List<WorkTask> tasks = new List<WorkTask>
        {
            //user 1
            new WorkTask {Title = "Finish report", Description = "Complete the monthly sales report", StartDate = DateTime.Now, UserId = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), TagId = 4, CategoryId = 1},
            new WorkTask { Title = "Clean kitchen", Description = "Wash dishes and clean counters", StartDate = DateTime.Now, UserId = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), TagId = 2, CategoryId = 2 },
            new WorkTask { Title = "Study for exam", Description = "Review notes for history exam", StartDate = DateTime.Now, UserId = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), TagId = 3, CategoryId = 3 },
            new WorkTask { Title = "Buy groceries", Description = "Buy milk, bread, and eggs", StartDate = DateTime.Now, UserId = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), TagId = 4, CategoryId = 4 },
            new WorkTask { Title = "Call mom", Description = "Call mom to catch up", StartDate = DateTime.Now, UserId = Guid.Parse("6e8b45b7-4e3f-4d21-a60e-8e81390e8f59"), TagId = 5, CategoryId = 5 },
            //user 2
            new WorkTask { Title = "Finish report", Description = "Complete the monthly sales report", StartDate = DateTime.Now, UserId = Guid.Parse("2e5c7dbf-82e1-45b0-9be0-8e7ba8e57b11"), TagId = 1, CategoryId = 1 },
            new WorkTask { Title = "Clean kitchen", Description = "Wash dishes and clean counters", StartDate = DateTime.Now, UserId = Guid.Parse("2e5c7dbf-82e1-45b0-9be0-8e7ba8e57b11"), TagId = 2, CategoryId = 2 },
            new WorkTask { Title = "Study for exam", Description = "Review notes for history exam", StartDate = DateTime.Now, UserId = Guid.Parse("2e5c7dbf-82e1-45b0-9be0-8e7ba8e57b11"), TagId = 3, CategoryId = 3 },
            new WorkTask { Title = "Buy groceries", Description = "Buy milk, bread, and eggs", StartDate = DateTime.Now, UserId = Guid.Parse("2e5c7dbf-82e1-45b0-9be0-8e7ba8e57b11"), TagId = 4, CategoryId = 4 },
            //user 3
            new WorkTask { Title = "Finish report", Description = "Complete the monthly sales report", StartDate = DateTime.Now, UserId = Guid.Parse("f5d3c88f-902b-4d8f-85e5-41bcf5d2cbb1"), TagId = 1, CategoryId = 1 },
            new WorkTask { Title = "Clean kitchen", Description = "Wash dishes and clean counters", StartDate = DateTime.Now, UserId = Guid.Parse("f5d3c88f-902b-4d8f-85e5-41bcf5d2cbb1"), TagId = 2, CategoryId = 2 },
            new WorkTask { Title = "Study for exam", Description = "Review notes for history exam", StartDate = DateTime.Now, UserId = Guid.Parse("f5d3c88f-902b-4d8f-85e5-41bcf5d2cbb1"), TagId = 3, CategoryId = 3 },
            new WorkTask { Title = "Buy groceries", Description = "Buy milk, bread, and eggs", StartDate = DateTime.Now, UserId = Guid.Parse("f5d3c88f-902b-4d8f-85e5-41bcf5d2cbb1"), TagId = 4, CategoryId = 4 },
            //user 4
            new WorkTask { Title = "Finish report", Description = "Complete the monthly sales report", StartDate = DateTime.Now, UserId=Guid.Parse("94a860d3-28d6-4fe6-ae0d-ebf47d799c51"), TagId=1, CategoryId=1},
            new WorkTask { Title="Clean kitchen", Description="Wash dishes and clean counters", StartDate=DateTime.Now, UserId=Guid.Parse("94a860d3-28d6-4fe6-ae0d-ebf47d799c51"), TagId=2, CategoryId=2},
            new WorkTask { Title="Study for exam", Description="Review notes for history exam", StartDate=DateTime.Now, UserId=Guid.Parse("94a860d3-28d6-4fe6-ae0d-ebf47d799c51"), TagId=3, CategoryId=3},
            new WorkTask { Title="Buy groceries", Description="Buy milk, bread, and eggs", StartDate=DateTime.Now, UserId=Guid.Parse("94a860d3-28d6-4fe6-ae0d-ebf47d799c51"), TagId=4, CategoryId=4},
            //user 5
            new WorkTask { Title="Finish report", Description="Complete the monthly sales report", StartDate=DateTime.Now, UserId=Guid.Parse("1f73db4e73e34397b9602c68b60792d4"), TagId=1, CategoryId=1},
            new WorkTask { Title="Clean kitchen", Description="Wash dishes and clean counters", StartDate=DateTime.Now, UserId=Guid.Parse("1f73db4e73e34397b9602c68b60792d4"), TagId=2, CategoryId=2},
            new WorkTask { Title="Study for exam", Description="Review notes for history exam", StartDate=DateTime.Now, UserId=Guid.Parse("1f73db4e73e34397b9602c68b60792d4"), TagId=3, CategoryId=3},
            new WorkTask { Title="Buy groceries", Description="Buy milk, bread, and eggs", StartDate=DateTime.Now, UserId=Guid.Parse("1f73db4e73e34397b9602c68b60792d4"), TagId=4, CategoryId=4}
        };

        _context.WorkTasks.AddRange(tasks);
        _context.SaveChanges();
    }
}