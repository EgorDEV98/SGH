using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Data.Entities;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы устройства
/// </summary>
public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        var dateTimeNow = DateTime.Now;
        
        builder.ToTable("Devices");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasDefaultValue("Новое устройство");
        builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(dateTimeNow);
        builder.Property(x => x.LastUpdated).IsRequired().HasDefaultValue(dateTimeNow);
        builder.Property(x => x.DeleteDate);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.IsDeleted);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.UserId);
    }
}