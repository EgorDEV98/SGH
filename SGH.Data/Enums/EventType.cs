using System.Runtime.Serialization;

namespace SGH.Data.Enums;

/// <summary>
/// Тип события
/// </summary>
public enum EventType
{
    /// <summary>
    /// Запрос
    /// </summary>
    [EnumMember(Value = "REQUEST")]
    Request,
    
    /// <summary>
    /// Подтверждение
    /// </summary>
    [EnumMember(Value = "CONFIRMATION")]
    Confirmation,
}