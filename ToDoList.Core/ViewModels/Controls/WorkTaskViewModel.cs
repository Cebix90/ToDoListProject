using ToDoList.Core.Models.Base;

namespace ToDoList.Core.Models.Controls;

public class WorkTaskViewModel : BaseViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsFinalized { get; set; }
    public string Status { get; set; }
    public string Category { get; set; }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged("IsSelected");
        }
    }
}