using System.ComponentModel.DataAnnotations;

namespace ToDoList.Database.Entities;

public class WorkTask
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsSelected { get; set; }
    public bool IsFinalized { get; set; }
    public User? User { get; set; }
    public Guid? UserId { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public Tag? Tag { get; set; }
    public int? TagId { get; set; }
    public Category? Category { get; set; }
    public int? CategoryId { get; set; }

}
