using System.Runtime.Serialization;

namespace SGH.Data.Enums;

/// <summary>
/// Состояние актуатора
/// </summary>
public enum ActuatorState
{
    /// <summary>
    /// Включен
    /// </summary>
    [EnumMember(Value = "ENABLED")]
    Enabled,
    
    /// <summary>
    /// Выключен
    /// </summary>
    [EnumMember(Value = "DISABLED")]
    Disabled,
    
    /// <summary>
    /// Реверс
    /// </summary>
    [EnumMember(Value = "REVERSE")]
    Reverse,
}