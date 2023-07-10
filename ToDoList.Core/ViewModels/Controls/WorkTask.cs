namespace ToDoList.DataAccess.ViewModels.Entities;

public class WorkTask : BaseViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsSelected { get; set; }

}