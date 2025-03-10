using System.Runtime.Serialization;

namespace SGH.Data.Enums;

/// <summary>
/// Наименование события
/// </summary>
public enum EventName
{
    /// <summary>
    /// Онлайн
    /// </summary>
    [EnumMember(Value = "ONLINE")]
    Online,
    
    /// <summary>
    /// Offline
    /// </summary>
    [EnumMember(Value = "OFFLINE")]
    Offline,
    
    /// <summary>
    /// Регистрация устройства
    /// </summary>
    [EnumMember(Value = "REGISTRATION")]
    Registration,
    
    /// <summary>
    /// Сброс устройства
    /// </summary>
    [EnumMember(Value = "RESET")]
    Reset,
    
    /// <summary>
    /// Добавления датчика
    /// </summary>
    [EnumMember(Value = "ADD_SENSOR")]
    AddSensor,
    
    /// <summary>
    /// Удаления датчика
    /// </summary>
    [EnumMember(Value = "REMOVE_SENSOR")]
    RemoveSensor,
    
    /// <summary>
    /// Добавления показания датчика
    /// </summary>
    [EnumMember(Value = "ADD_SENSOR_MEASUREMENT")]
    AddSensorMeasurement,
    
    /// <summary>
    /// Добавление актуатора
    /// </summary>
    [EnumMember(Value = "ADD_ACTUATOR")]
    AddActuator,
    
    /// <summary>
    /// Удаление актуатора
    /// </summary>
    [EnumMember(Value = "REMOVE_ACTUATOR")]
    RemoveActuator,
    
    /// <summary>
    /// Смена состояния актуатора
    /// </summary>
    [EnumMember(Value = "ACTUATOR_CHANGE_STATE")]
    ActuatorChangeState,
}