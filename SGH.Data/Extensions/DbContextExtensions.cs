﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGH.Data.Settings;

namespace SGH.Data.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetSection(nameof(PostgresSettings))
            .GetSection(nameof(PostgresSettings.ConnectionString))
            .Value ?? throw new NullReferenceException("PostgresSettings not found");
        
        serviceCollection.AddDbContext<PostgresDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
        return serviceCollection;
    }
    
    public static async Task ApplyMigrationAsync(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var dbContextReserve = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
        try
        {
            await dbContextReserve.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception {nameof(PostgresDbContext)}: {exception.Message}");
            Console.WriteLine($"Ошибка: при создании базы данных {exception.Message}");
            Console.WriteLine($"{exception?.InnerException?.Message}");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Выход из приложения exit(1), сервер ASPNET не запущен!");
            Environment.Exit(333);
        }
    }
}