using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;

namespace SGH.Application.Services;

public class UsersService : IUsersService
{
    private readonly PostgresDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtProvider _jwtProvider;
    private readonly UserMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UsersService(PostgresDbContext context, IDateTimeProvider dateTimeProvider, IJwtProvider jwtProvider, UserMapper mapper, IPasswordHasher passwordHasher)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
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

        if (!_passwordHasher.VerifyPassword(param.Password, user!.Password))
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
        var password = _passwordHasher.HashPassword(param.Password);
        
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

    /// <summary>
    /// Обновить данные пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetUserInfoResponse> UpdateUser(UpdateUserParams param, CancellationToken ct)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == param.UserId, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"User Id({param.UserId}) is not found");
        }

        if (!string.IsNullOrWhiteSpace(param.Name))
        {
            entity!.Name = param.Name;
        }
        
        if (!string.IsNullOrWhiteSpace(param.Password))
        {
            entity!.Password = _passwordHasher.HashPassword(param.Password);
        }
        
        await _context.SaveChangesAsync(ct);
        return _mapper.Map(entity!);
    }
}