using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Data.Entities;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы пользователя
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasDefaultValue("Пользователь");
        builder.Property(x => x.Login).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdated).IsRequired();
    }
}