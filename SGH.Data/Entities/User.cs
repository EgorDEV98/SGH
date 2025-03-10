using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : IEntityDate
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login  { get; set; }
    
    /// <summary>
    /// Зашифрованный пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Устройства пользователя
    /// </summary>
    public ICollection<Device> Devices { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdated { get; set; }
}