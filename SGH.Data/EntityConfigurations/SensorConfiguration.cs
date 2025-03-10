using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Data.Entities;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы датчиков
/// </summary>
public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        var dateTimeNow = DateTime.Now;
        
        builder.ToTable("Sensors");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.SystemName).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(dateTimeNow);
        builder.Property(x => x.LastUpdated).IsRequired().HasDefaultValue(dateTimeNow);
        builder.Property(x => x.DeleteDate);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Sensors)
            .HasForeignKey(x => x.DeviceId);
    }
}