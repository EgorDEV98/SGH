using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;
using SGH.Data.Enums;

namespace SGH.Application.Services;

public class ActuatorsService : IActuatorsService
{
    private readonly PostgresDbContext _context;
    private readonly ActuatorMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ActuatorsService(PostgresDbContext context, ActuatorMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    /// <summary>
    /// Получить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> GetActuator(GetActuatorParams param, CancellationToken ct)
    {
        var entity = await _context.Actuators.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Actuator Id({param.Id}) was not found)");
        }
        
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить все актуаторы на устройстве
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetActuatorResponse>> GetActuators(GetActuatorsParams param, CancellationToken ct)
    {
        var entities = await _context.Actuators
            .Where(x => x.DeviceId == param.DeviceId)
            .ToArrayAsync(ct);
        return _mapper.Map(entities);
    }

    /// <summary>
    /// Добавить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> AddActuator(AddActuatorParams param, CancellationToken ct)
    {
        var currentDevice = await _context.Devices
            .Include(x => x.Actuators)
            .FirstOrDefaultAsync(x => x.Id == param.DeviceId, ct);
        if (currentDevice is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) was not found");
        }

        var currentDate = _dateTimeProvider.GetCurrent();
        var newActuator = new Actuator()
        {
            Name = param.Name,
            SystemName = param.SystemName,
            State = ActuatorState.Disabled,
            DeviceId = param.DeviceId,
            LastUpdated = currentDate,
            CreatedDate = currentDate
        };
        
        await _context.Actuators.AddAsync(newActuator, ct);
        await _context.SaveChangesAsync(ct);
        
        return _mapper.Map(newActuator);
    }

    /// <summary>
    /// Обновить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> UpdateActuator(UpdateActuatorParams param, CancellationToken ct)
    {
        var actuator = await _context.Actuators
            .Where(x => x.Id == param.Id)
            .FirstOrDefaultAsync(ct);
        if (actuator is null)
        {
            NotFoundException.Throw($"Actuator Id({param.Id}) was not found");
        }

        if (!string.IsNullOrWhiteSpace(param.Name))
        {
            actuator!.Name = param.Name;
        }
        if (param.State.HasValue)
        {
            actuator!.State = param.State.Value;
        }

        actuator!.LastUpdated = _dateTimeProvider.GetCurrent();
        
        await _context.SaveChangesAsync(ct);
        
        return _mapper.Map(actuator!);
    }

    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="param"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> DeleteActuator(DeleteActuatorParams param, CancellationToken ct)
    {
        var entity = await _context.Actuators
            .Where(x => x.Id == param.Id)
            .FirstOrDefaultAsync(ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Actuator Id({param.Id}) was not found");
        }
        
        entity!.LastUpdated = _dateTimeProvider.GetCurrent();
        entity.IsDeleted = true;
        entity.DeleteDate = _dateTimeProvider.GetCurrent();
        await _context.SaveChangesAsync(ct);

        return true;
    }
}