using Microsoft.AspNetCore.Mvc;
using SGH.Application.Interfaces;
using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;
using SGH.WebApi.Mappers;

namespace SGH.WebApi.Controllers;

/// <summary>
/// Контроллер авторизации пользователей
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase, IUserController
{
    private readonly IUsersService _usersService;
    private readonly AuthMapper _mapper;

    public UsersController(IUsersService usersService, AuthMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }

    /// <summary>
    /// Авторизация в системе
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<AuthResponse> Login([FromBody] AuthRequest request, CancellationToken ct)
        => await _usersService.Login(_mapper.Map(request), ct);

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("registration")]
    public async Task<RegistrationResponse> Registration([FromBody] RegistrationRequest request, CancellationToken ct)
        => await _usersService.Registration(_mapper.Map(request), ct);
}