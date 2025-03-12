using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

/// <summary>
/// Интерфейс контроллера датчиков
/// </summary>
public interface ISensorsController
{
    /// <summary>
    /// Получить датчик по Id
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> GetSensor(Guid id, CancellationToken ct); 
    
    /// <summary>
    /// Получить все датчики
    /// </summary>
    /// <param name="deviceId">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetSensorResponse>> GetSensors(Guid deviceId, CancellationToken ct);

    /// <summary>
    /// Добавить датчик
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> AddSensor(Guid deviceId, AddSensorRequest param, CancellationToken ct);

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> UpdateSensor(Guid id, UpdateSensorRequest request, CancellationToken ct);
    
    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task DeleteSensor(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить показания с датчиков
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValues(Guid sensorId, GetSensorValuesRequest request, CancellationToken ct);

    /// <summary>
    /// Добавить показание датчика
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task AddSensorValue(Guid sensorId, AddSensorValuesRequest param, CancellationToken ct);
}