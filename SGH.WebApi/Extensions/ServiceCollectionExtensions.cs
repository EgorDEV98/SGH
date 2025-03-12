using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.JWT;
using SGH.Application.Services;
using SGH.WebApi.Mappers;

namespace SGH.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<AuthMapper>();
        services.AddSingleton<DeviceMapper>();
        services.AddSingleton<UserMapper>();
        services.AddSingleton<ActuatorMapper>();
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesesService>();
        services.AddScoped<IActuatorsService, ActuatorsService>();
        
        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
        services.AddAuthorization();
        
        return services;
    }
}