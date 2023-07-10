namespace ToDoList.Core.Models.Controls;

public class CommentViewModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public virtual UserViewModel Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}