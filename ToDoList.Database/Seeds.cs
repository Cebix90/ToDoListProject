using ToDoList.Database.Entities;

namespace ToDoList.Database;

public class Seeds
{
    private readonly ToDoListDbContext _context;
    public Seeds(ToDoListDbContext context)
    {
        _context = context;
    }

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
    }

    private void SeedUsers()
    {
        List<User> users = new List<User>
        {
            new User { Email = "jan.kowalski@example.com", Password = "hasło123", NickName = "JKowal", Country = "Polska" },
            new User { Email = "anna.nowak@example.com", Password = "mojeHasło", NickName = "ANowak", Country = "Polska" },
            new User { Email = "marek123@example.com", Password = "tajne123", NickName = "Marek123", Country = "Polska" },
            new User { Email = "kasia87@example.com", Password = "bezpieczneHasło", NickName = "Kasia87", Country = "Polska" },
            new User { Email = "adam.jankowski@example.com", Password = "haslo2023", NickName = "AJank", Country = "Polska" },
            new User { Email = "ewa1234@example.com", Password = "mojeTajemnice", NickName = "Ewa1234", Country = "Polska" },
            new User { Email = "piotr.k@example.com", Password = "12345678", NickName = "Piotrek", Country = "Polska" },
            new User { Email = "magda.m@example.com", Password = "magda!123", NickName = "MagdaM", Country = "Polska" },
            new User { Email = "bartek.nowacki@example.com", Password = "haselko321", NickName = "BNowacki", Country = "Polska" },
            new User { Email = "iza.kowalska@example.com", Password = "mojeHaslo123", NickName = "IzaK", Country = "Polska" }
        };

        _context.Users.AddRange(users);
        _context.SaveChanges();
    }

    private void SeedCategories()
    {
        List<Category> categories = new List<Category>
        {
            new Category { Value = "Praca" },
            new Category { Value = "Dom" },
            new Category { Value = "Szkoła" },
            new Category { Value = "Zakupy" },
            new Category { Value = "Inne" }
        };

        _context.Categories.AddRange(categories);
        _context.SaveChanges();
    }

    private void SeedTags()
    {
        List<Tag> tags = new List<Tag>
        {
            new Tag { Value = "Wysoki priorytet" },
            new Tag { Value = "Średni priorytet" },
            new Tag { Value = "Niski priorytet" },
            new Tag { Value = "Pilne" },
            new Tag { Value = "Oczekujące" },
            new Tag { Value = "Zakończone" }
        };

        _context.Tags.AddRange(tags);
        _context.SaveChanges();
    }
}