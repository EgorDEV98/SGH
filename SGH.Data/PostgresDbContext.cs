using Microsoft.EntityFrameworkCore;
using SGH.Data.Entities;
using SGH.Data.EntityConfigurations;

namespace SGH.Data;

/// <summary>
/// Контекст БД
/// </summary>
public class PostgresDbContext : DbContext
{
    /// <summary>
    /// Пользователя
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Устройства
    /// </summary>
    public DbSet<Device> Devices { get; set; }
    
    /// <summary>
    /// Датчики
    /// </summary>
    public DbSet<Sensor> Sensors { get; set; }
    
    /// <summary>
    /// Показания датчиков
    /// </summary>
    public DbSet<SensorValue> SensorValues { get; set; }
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public DbSet<Actuator> Actuators { get; set; }

    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new DeviceConfiguration());
        builder.ApplyConfiguration(new SensorConfiguration());
        builder.ApplyConfiguration(new SensorValueConfiguration());
        builder.ApplyConfiguration(new ActuatorConfiguration());
    }
}