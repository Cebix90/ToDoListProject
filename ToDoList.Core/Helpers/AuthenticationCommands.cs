using ToDoList.Database;

namespace ToDoList.Core.Helpers;

public class AuthenticationCommands
{
    private readonly ToDoListDbContext _context;

    public AuthenticationCommands(ToDoListDbContext context)
    {
        _context = context;
    }

    public bool AuthenticateUser(string email, string password)
    {
        bool isAuthenticated = _context.Users.Any(u => u.Email == email && u.Password == password);
        return isAuthenticated;
    }
}