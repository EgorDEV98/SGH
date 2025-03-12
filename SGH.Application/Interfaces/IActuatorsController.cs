using SGH.Application.Models.Params;
using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

/// <summary>
/// Интерфейс контроллра актуаторов
/// </summary>
public interface IActuatorsController
{
    /// <summary>
    /// Получить актуатор
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> GetActuator(Guid id, CancellationToken ct);
    
    /// <summary>
    /// Получить все актуаторы устройства
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetActuatorResponse>> GetActuators(Guid deviceId, CancellationToken ct);

    /// <summary>
    /// Добавление актуатора
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> AddActuator(AddActuatorRequest param, CancellationToken ct);

    /// <summary>
    /// Обновление актуатора
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<GetActuatorResponse> UpdateActuator(Guid id, UpdateActuatorRequest param, CancellationToken ct);
    
    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task DeleteActuator(Guid id, CancellationToken ct);
}