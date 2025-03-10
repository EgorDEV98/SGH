using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Models.JWT;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;

namespace SGH.Application.Services;

public class AuthService : IAuthService
{
    private readonly PostgresDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuthService(PostgresDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<AuthResponse> Login(AuthParams param, CancellationToken ct)
    {
        var password = DataEncryptor.Encrypt(param.Password);
        var user = await _context.Users
            .Where(x => x.Password == password)
            .Where(x => x.Login == param.Login)
            .FirstOrDefaultAsync(ct);
        if (user is null)
        {
            NotFoundException.Throw("Invalid login or password");
        }

        return new AuthResponse()
        {
            Token = CreateJwt(user!)
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
        var password = DataEncryptor.Encrypt(param.Password);

        var loginIsExist = await _context.Users.AnyAsync(x => x.Login == param.Login, ct);
        if (loginIsExist)
        {
            ConflictException.Throw("User already exists");
        }

        var dateTimeNow = _dateTimeProvider.GetCurrent();
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
            Token = CreateJwt(newUser)
        };
    }

    private string CreateJwt(User user)
    {
        var nowDate = _dateTimeProvider.GetCurrent();
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: nowDate,
            claims: new List<Claim>()
            {
                new Claim(nameof(User.Id), user.Id.ToString()),
                new Claim(nameof(User.Name), user.Name)
            },
            expires: nowDate.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}