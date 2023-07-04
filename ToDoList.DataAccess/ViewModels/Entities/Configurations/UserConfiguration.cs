using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DataAccess.ViewModels.Entities;

namespace ToDoList.DataAccess.ViewModels.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Task)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}