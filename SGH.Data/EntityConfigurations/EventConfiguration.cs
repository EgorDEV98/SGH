using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGH.Data.Convertors;
using SGH.Data.Entities;
using SGH.Data.Enums;

namespace SGH.Data.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы событий
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        var eventNameConverter = new ValueConverter<EventName, string>(
            x => EnumConverters.ToEnumString(x),
            x => EnumConverters.ToEnum<EventName>(x),
            new ConverterMappingHints(size: 50, unicode: false));
        
        var eventTypeConverter = new ValueConverter<EventType, string>(
            x => EnumConverters.ToEnumString(x),
            x => EnumConverters.ToEnum<EventType>(x),
            new ConverterMappingHints(size: 50, unicode: false));
        
        builder.ToTable("Events");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventName).IsRequired().HasConversion(eventNameConverter);
        builder.Property(x => x.EventType).IsRequired().HasConversion(eventTypeConverter);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdated).IsRequired();

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.DeviceId);
    }
}