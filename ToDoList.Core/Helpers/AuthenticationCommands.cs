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

    public Guid GetUserIdByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            return user.Id;
        }
        else
        {
            return Guid.Empty;
        }
    }
}