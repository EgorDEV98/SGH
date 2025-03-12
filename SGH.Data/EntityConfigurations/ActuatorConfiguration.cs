using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGH.Data.Convertors;
using SGH.Data.Entities;
using SGH.Data.Enums;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы актуаторов
/// </summary>
public class ActuatorConfiguration : IEntityTypeConfiguration<Actuator>
{
    public void Configure(EntityTypeBuilder<Actuator> builder)
    {
        var stateConverter = new ValueConverter<ActuatorState, string>(
            x => EnumConverters.ToEnumString(x),
            x => EnumConverters.ToEnum<ActuatorState>(x),
            new ConverterMappingHints(size: 50, unicode: false));
        
        builder.ToTable("Actuators");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SystemName).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasDefaultValue("Новый актуатор");
        builder.Property(x => x.State).IsRequired().HasConversion(stateConverter);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdated).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.DeleteDate);
        
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Actuators)
            .HasForeignKey(x => x.DeviceId);
    }
}