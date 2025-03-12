using SGH.Application.Interfaces;
using SGH.Application.Models.Params;
using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.WebApi.Controllers;

public class SensorsController : BaseAuthController, ISensorsController
{
    private readonly ISensorsService _sensorsService;

    public SensorsController(ISensorsService sensorsService)
    {
        _sensorsService = sensorsService;
    }

    /// <summary>
    /// Получить датчик по Id
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> GetSensor(Guid id, CancellationToken ct)
        => await _sensorsService.GetSensor(new GetSensorParams()
        {
            Id = id,
        }, ct);

    /// <summary>
    /// Получить все датчики
    /// </summary>
    /// <param name="deviceId">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorResponse>> GetSensors(Guid deviceId, CancellationToken ct)
        => await _sensorsService.GetSensors(new GetSensorsParams()
        {
            DeviceId = deviceId,
        }, ct);

    /// <summary>
    /// Добавить датчик
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> AddSensor(Guid deviceId, AddSensorRequest param, CancellationToken ct)
        => await _sensorsService.AddSensor(new AddSensorParams()
        {
            DeviceId = deviceId,
            Name = param.Name,
            SystemName = param.SystemName
        }, ct);

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> UpdateSensor(Guid id, UpdateSensorRequest request, CancellationToken ct)
        => await _sensorsService.UpdateSensor(new UpdateSensorParams()
        {
            Id = id,
            Name = request.Name,
        }, ct);

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task DeleteSensor(Guid id, CancellationToken ct)
        => await _sensorsService.DeleteSensor(new DeleteSensorParams()
        {
            Id = id
        }, ct);

    /// <summary>
    /// Получить показания с датчиков
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValues(Guid sensorId,
        GetSensorValuesRequest request, CancellationToken ct)
        => await _sensorsService.GetSensorValues(new GetSensorValuesParams()
        {
            Id = sensorId,
            From = request.From,
            To = request.To,
            Limit = request.Limit,
            Offset = request.Offset,
        }, ct);

    /// <summary>
    /// Добавить показание датчика
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task AddSensorValue(Guid sensorId, AddSensorValuesRequest param, CancellationToken ct)
        => await _sensorsService.AddSensorValue(new AddSensorValueParams()
        {
            Id = sensorId,
            Value = param.Value,
            MeasurementDate = param.MeasurementDate,
        }, ct);
}