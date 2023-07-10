namespace ToDoList.DataAccess.ViewModels.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string NickName { get; set; }
    public string Country { get; set; }

}