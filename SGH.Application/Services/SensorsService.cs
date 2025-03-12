using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;
using SGH.Data.Extensions;

namespace SGH.Application.Services;

/// <summary>
/// Сервис дачтиков
/// </summary>
public class SensorsService : ISensorsService
{
    private readonly PostgresDbContext _context;
    private readonly SensorMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SensorsService(PostgresDbContext context, SensorMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Получить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> GetSensor(GetSensorParams param, CancellationToken ct)
    {
        var entity = await _context.Sensors
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Sensor Id({param.Id}) was not found");
        }
        
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить список датчиков устройства
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorResponse>> GetSensors(GetSensorsParams param, CancellationToken ct)
    {
        var device = await _context.Devices
            .Include(x => x.Sensors)
            .FirstOrDefaultAsync(x => x.Id == param.DeviceId, ct);
        if (device is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) was not found");
        }

        return _mapper.Map(device!.Sensors ?? []);
    }

    /// <summary>
    /// Добавить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> AddSensor(AddSensorParams param, CancellationToken ct)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(x => x.Id == param.DeviceId, ct);
        if (device is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) was not found");
        }

        var currentDate = _dateTimeProvider.GetCurrent();
        var newSensor = new Sensor()
        {
            SystemName = param.SystemName,
            Name = param.Name,
            Device = device!,
            DeviceId = param.DeviceId,
            CreatedDate = currentDate,
            LastUpdated = currentDate
        };
        
        await _context.Sensors.AddAsync(newSensor, ct);
        await _context.SaveChangesAsync(ct);
        
        return _mapper.Map(newSensor);
    }

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> UpdateSensor(UpdateSensorParams param, CancellationToken ct)
    {
        var entity = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Sensor Id({param.Id}) was not found");
        }

        if (param.Name is not null)
        {
            entity!.Name = param.Name;
            entity.LastUpdated = _dateTimeProvider.GetCurrent();
        }
        
        await _context.SaveChangesAsync(ct);
        
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteSensor(DeleteSensorParams param, CancellationToken ct)
    {
        var entity = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Sensor Id({param.Id}) was not found");
        }
        
        var currentTime = _dateTimeProvider.GetCurrent();
        entity!.IsDeleted = true;
        entity!.LastUpdated = currentTime;
        entity!.DeleteDate = currentTime;
        await _context.SaveChangesAsync(ct);
        
        return true;
    }

    /// <summary>
    /// Получить значения с датчиков
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValues(GetSensorValuesParams param, CancellationToken ct)
    {
        var isExistSensor = await _context.Sensors.AnyAsync(x => x.Id == param.Id, ct);
        if (!isExistSensor)
        {
            NotFoundException.Throw($"Sensor Id({param.Id}) was not found");
        }
        
        var entities = await _context.SensorValues
            .Where(x => x.SensorId == param.Id)
            .WhereIf(param.From.HasValue, x => x.MeasurementDate >= param.From)
            .WhereIf(param.To.HasValue, x => x.MeasurementDate <= param.To)
            .Skip(param.Offset ?? 0)
            .Take(param.Limit ?? 100)
            .ToArrayAsync(ct);

        return _mapper.Map(entities);
    }

    /// <summary>
    /// Добавить показания датчика
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> AddSensorValue(AddSensorValueParams param, CancellationToken ct)
    {
        var entity = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Sensor Id({param.Id}) was not found");
        }
        
        var newSensorValue = new SensorValue()
        {
            MeasurementDate = param.MeasurementDate,
            Value = param.Value,
            SensorId = entity!.Id,
        };
        
        await _context.SensorValues.AddAsync(newSensorValue, ct);
        await _context.SaveChangesAsync(ct);
        
        return true;
    }
}