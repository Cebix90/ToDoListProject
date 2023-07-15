using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Entities.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Category"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(c => c.Tasks)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);

        /*builder.Property(x => x.Value)
            .IsRequired();*/
    }
}