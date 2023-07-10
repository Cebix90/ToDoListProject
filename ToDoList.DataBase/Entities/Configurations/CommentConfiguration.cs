using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DataBase.Entities.Configurations;
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId);

        builder.Property(x => x.CreatedDate)
            .HasDefaultValueSql("getutcdate()");
        builder.Property(x => x.UpdatedDate)
            .ValueGeneratedOnUpdate();
    }
}