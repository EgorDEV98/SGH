﻿using SGH.Data.Enums;
using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Актуаторы
/// </summary>
public class Actuator : IEntityDate, ISoftDeleteEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Системное имя актуатора
    /// </summary>
    public string SystemName { get; set; }
    
    /// <summary>
    /// Текущее состояние
    /// </summary>
    public ActuatorState State { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }

    /// <summary>
    /// Дата добавления актуатора
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата обновления актуатор
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    /// Признак мягкого удаления
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime DeleteDate { get; set; }
}