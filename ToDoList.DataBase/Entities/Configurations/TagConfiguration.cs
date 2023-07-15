using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Entities.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Tag"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(t => t.Tasks)
            .WithOne(t => t.Tag)
            .HasForeignKey(t => t.TagId);
    }
}