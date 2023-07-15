using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Entities.Configurations;
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Comment"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .IsRequired(false);

        builder.Property(x => x.CreatedDate)
            .HasDefaultValueSql("getutcdate()");
        builder.Property(x => x.UpdatedDate)
            .ValueGeneratedOnUpdate();
    }
}