using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Entities.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="User"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Task)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}