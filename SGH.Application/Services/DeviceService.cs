using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;
using SGH.Data;
using SGH.Data.Entities;

namespace SGH.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly PostgresDbContext _postgresDbContext;
    private readonly DeviceMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeviceService(PostgresDbContext postgresDbContext, DeviceMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _postgresDbContext = postgresDbContext;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Получить устройство по Id
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> GetDevice(GetDeviceParams param, CancellationToken ct)
    {
        var entity = await _postgresDbContext.Devices
            .Where(x => x.Id == param.DeviceId)
            .Where(x => x.UserId == param.UserId)
            .FirstOrDefaultAsync(ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) not found)");
        }
        
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить список устройств пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetDeviceResponse>> GetDevices(GetDevicesParams param, CancellationToken ct)
    {
        var entities = await _postgresDbContext.Devices
            .Where(x => x.UserId == param.UserId)
            .ToArrayAsync(ct);
        return _mapper.Map(entities);
    }

    /// <summary>
    /// Добавить устройство пользователю
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> AddDevice(AddDeviceParams param, CancellationToken ct)
    {
        var newDevice = new Device()
        {
            UserId = param.UserId,
            Name = param.Name ?? "Новое устройство",
            CreatedDate = _dateTimeProvider.GetCurrent(),
            LastUpdated = _dateTimeProvider.GetCurrent(),
        };
        
        await _postgresDbContext.Devices.AddAsync(newDevice, ct);
        await _postgresDbContext.SaveChangesAsync(ct);
        
        return _mapper.Map(newDevice);
    }

    /// <summary>
    /// Обновить данные об устройстве
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> UpdateDevice(UpdateDeviceParams param, CancellationToken ct)
    {
        var entity = await _postgresDbContext.Devices
            .Where(x => x.Id == param.DeviceId)
            .Where(x => x.UserId == param.UserId)
            .FirstOrDefaultAsync(ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) not found");
        }

        if (param.Name is not null)
        {
            entity!.Name = param.Name;
            entity!.LastUpdated = _dateTimeProvider.GetCurrent();
        }
        
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Удалить устройство
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteDevice(DeleteDeviceParams param, CancellationToken ct)
    {
        var entity = await _postgresDbContext.Devices
            .Where(x => x.Id == param.DeviceId)
            .Where(x => x.UserId == param.UserId)
            .FirstOrDefaultAsync(ct);
        if (entity is null)
        {
            NotFoundException.Throw($"Device Id({param.DeviceId}) not found");
        }
        
        entity!.IsDeleted = true;
        entity!.DeleteDate = _dateTimeProvider.GetCurrent();
        
        await _postgresDbContext.SaveChangesAsync(ct);
        return true;
    }
}