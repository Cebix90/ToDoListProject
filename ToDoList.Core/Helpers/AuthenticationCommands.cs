using ToDoList.Database;

namespace ToDoList.Core.Helpers;

/// <summary>
/// Class containing methods for user authentication.
/// </summary>
public class AuthenticationCommands
{
    private readonly ToDoListDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationCommands"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AuthenticationCommands(ToDoListDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Authenticates a user based on the provided email and password.
    /// </summary>
    /// <param name="email">The user's email.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>True if the authentication is successful; otherwise, false.</returns>
    public virtual bool AuthenticateUser(string email, string password)
    {
        bool isAuthenticated = _context.Users.Any(u => u.Email == email && u.Password == password);
        return isAuthenticated;
    }


    /// <summary>
    /// Retrieves the user ID based on the provided email.
    /// </summary>
    /// <param name="email">The user's email.</param>
    /// <returns>The user ID or Guid.Empty if the user is not found.</returns>
    public virtual Guid GetUserIdByEmail(string email)
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