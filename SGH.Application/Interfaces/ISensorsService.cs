using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса датчиков
/// </summary>
public interface ISensorsService
{
    /// <summary>
    /// Получить датчик по Id
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> GetSensor(GetSensorParams param, CancellationToken ct); 
    
    /// <summary>
    /// Получить все датчики
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetSensorResponse>> GetSensors(GetSensorsParams param, CancellationToken ct);
    
    /// <summary>
    /// Добавить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> AddSensor(AddSensorParams param, CancellationToken ct);
    
    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetSensorResponse> UpdateSensor(UpdateSensorParams param, CancellationToken ct);
    
    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<bool> DeleteSensor(DeleteSensorParams param, CancellationToken ct);
    
    /// <summary>
    /// Получить показания с датчиков
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValues(GetSensorValuesParams param, CancellationToken ct);

    /// <summary>
    /// Добавить показание датчика
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<bool> AddSensorValue(AddSensorValueParams param, CancellationToken ct);
}