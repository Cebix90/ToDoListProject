﻿namespace ToDoList.Database.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public virtual User Author { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public WorkTask Task { get; set; }
    public int TaskId { get; set; }
}