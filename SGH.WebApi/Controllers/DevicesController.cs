using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Application.Interfaces;
using SGH.Application.Models.Params;
using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : BaseAuthController, IDevicesController
{
    private readonly IDevicesService _devicesService;

    public DevicesController(IDevicesService devicesService)
    {
        _devicesService = devicesService;
    }

    /// <summary>
    /// Получить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<GetDeviceResponse> GetDevice([FromRoute] Guid id, CancellationToken ct)
        => await _devicesService.GetDevice(new GetDeviceParams
        {
            UserId = base.UserId,
            DeviceId = id
        }, ct);

    /// <summary>
    /// Получить список устройств пользователя
    /// </summary>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IReadOnlyCollection<GetDeviceResponse>> GetDevices(CancellationToken ct)
        => await _devicesService.GetDevices(new GetDevicesParams()
        {
            UserId = base.UserId,
        }, ct);

    /// <summary>
    /// Добавить устройство пользователю
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<GetDeviceResponse> AddDevice([FromBody] AddDeviceRequest param, CancellationToken ct)
        => await _devicesService.AddDevice(new AddDeviceParams
        {
            UserId = base.UserId,
            Name = param.Name,
        }, ct);
    
    /// <summary>
    /// Обновить устройство пользователя
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<GetDeviceResponse> UpdateDevice([FromRoute] Guid id, [FromBody] UpdateDeviceRequest param, CancellationToken ct)
        => await _devicesService.UpdateDevice(new UpdateDeviceParams()
        {
            DeviceId = id,
            UserId = UserId,
            Name = param.Name,
        }, ct);

    /// <summary>
    /// Удалить устройство пользователя
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task DeleteDevice([FromRoute] Guid id, CancellationToken ct)
        => await _devicesService.DeleteDevice(new DeleteDeviceParams()
        {
            DeviceId = id,
            UserId = base.UserId,
        }, ct);
}