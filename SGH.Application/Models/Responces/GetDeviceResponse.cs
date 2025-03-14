﻿using SGH.Data.Enums;

namespace SGH.Application.Models.Responces;

public class GetDeviceResponse
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Датчики устройства
    /// </summary>
    public IReadOnlyCollection<GetSensorResponse> Sensors { get; set; }
        = new List<GetSensorResponse>();
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public IReadOnlyCollection<GetActuatorResponse> Actuators { get; set; }
        = new List<GetActuatorResponse>();
    
    /// <summary>
    /// Дата добавления устройства
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdated { get; set; }
}

public class GetSensorResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Системное имя датчика
    /// </summary>
    public string SystemName { get; set; }

    /// <summary>
    /// Дата добавления датчика
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdated { get; set; }
}