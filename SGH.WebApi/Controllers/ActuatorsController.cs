using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Application.Interfaces;
using SGH.Application.Models.Params;
using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActuatorsController : BaseAuthController, IActuatorsController
{
    private readonly IActuatorsService _actuatorsService;

    public ActuatorsController(IActuatorsService actuatorsService)
    {
        _actuatorsService = actuatorsService;
    }

    /// <summary>
    /// Получить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<GetActuatorResponse> GetActuator([FromRoute] Guid id, CancellationToken ct)
        => await _actuatorsService.GetActuator(new GetActuatorParams()
        {
            Id = id,
        }, ct);

    /// <summary>
    /// Получить список актуаторов устройства
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IReadOnlyCollection<GetActuatorResponse>> GetActuators([FromQuery] Guid deviceId, CancellationToken ct)
        => await _actuatorsService.GetActuators(new GetActuatorsParams()
        {
            DeviceId = deviceId,
        }, ct);

    /// <summary>
    /// Добавить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<GetActuatorResponse> AddActuator([FromBody] AddActuatorRequest param, CancellationToken ct)
        => await _actuatorsService.AddActuator(new AddActuatorParams()
        {
            DeviceId = param.DeviceId,
            SystemName = param.SystemName,
            Name = param.Name,
        }, ct);

    /// <summary>
    /// Обновление актуатора
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<GetActuatorResponse> UpdateActuator([FromRoute] Guid id, [FromBody] UpdateActuatorRequest param, CancellationToken ct)
        => await _actuatorsService.UpdateActuator(new UpdateActuatorParams()
        {
            Id = id,
            Name = param.Name,
            State = param.State,
        }, ct);

    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task DeleteActuator(Guid id, CancellationToken ct)
        => await _actuatorsService.DeleteActuator(new DeleteActuatorParams()
        {
            Id = id
        }, ct);
}