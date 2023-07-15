using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Entities.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(t => t.Tasks)
            .WithOne(t => t.Tag)
            .HasForeignKey(t => t.TagId);
    }
}