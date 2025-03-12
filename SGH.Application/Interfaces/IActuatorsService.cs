using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

/// <summary>
/// Интрфейса сервиса актуаторов
/// </summary>
public interface IActuatorsService
{
    /// <summary>
    /// Получить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> GetActuator(GetActuatorParams param, CancellationToken ct);
    
    /// <summary>
    /// Получить все актуаторы устройства
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetActuatorResponse>> GetActuators(GetActuatorsParams param, CancellationToken ct);
    
    /// <summary>
    /// Добавление актуатора
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> AddActuator(AddActuatorParams param, CancellationToken ct);
    
    /// <summary>
    /// Обновление актуатора
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> UpdateActuator(UpdateActuatorParams param, CancellationToken ct);
    
    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<bool> DeleteActuator(DeleteActuatorParams param, CancellationToken ct);
}