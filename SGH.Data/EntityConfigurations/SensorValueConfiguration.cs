using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Data.Entities;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы показаний датчиков
/// </summary>
public class SensorValueConfiguration : IEntityTypeConfiguration<SensorValue>
{
    public void Configure(EntityTypeBuilder<SensorValue> builder)
    {
        builder.ToTable("SensorValues");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Value).IsRequired();
        builder.Property(x => x.MeasurementDate).IsRequired();
        
        builder.HasOne(x => x.Sensor)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.SensorId);
    }
}