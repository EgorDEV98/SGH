using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;

namespace SGH.Application.Services;

public class AuthService : IAuthService
{
    private readonly PostgresDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(PostgresDbContext context, IDateTimeProvider dateTimeProvider, IJwtProvider jwtProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _jwtProvider = jwtProvider;
    }
    
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<AuthResponse> Login(AuthParams param, CancellationToken ct)
    {
        var user = await _context.Users
            .Where(x => x.Login == param.Login)
            .FirstOrDefaultAsync(ct);
        if (user is null)
        {
            UnauthorizedException.Throw("Invalid login or password");
        }

        if (!PasswordHasher.VerifyPassword(param.Password, user!.Password))
        {
            UnauthorizedException.Throw("Invalid login or password");
        }

        return new AuthResponse()
        {
            Token = _jwtProvider.GenerateJwtToken(user)
        };
    }

    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<RegistrationResponse> Registration(RegistrationParams param, CancellationToken ct)
    {
        var loginIsExist = await _context.Users.AnyAsync(x => x.Login == param.Login, ct);
        if (loginIsExist)
        {
            ConflictException.Throw("User already exists");
        }

        var dateTimeNow = _dateTimeProvider.GetCurrent();
        var password = PasswordHasher.HashPassword(param.Password);
        
        var newUser = new User()
        {
            Login = param.Login,
            Password = password,
            Name = param.Name,
            CreatedDate = dateTimeNow,
            LastUpdated = dateTimeNow,
        };
        await _context.Users.AddAsync(newUser, ct);
        await _context.SaveChangesAsync(ct);

        return new RegistrationResponse()
        {
            Token = _jwtProvider.GenerateJwtToken(newUser)
        };
    }
}