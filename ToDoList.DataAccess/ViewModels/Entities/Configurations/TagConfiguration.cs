using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DataAccess.ViewModels.Entities;

namespace ToDoList.DataAccess.ViewModels.Entities.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(t => t.Tasks)
            .WithOne(t => t.Tag)
            .HasForeignKey(t => t.TagId);
    }
}