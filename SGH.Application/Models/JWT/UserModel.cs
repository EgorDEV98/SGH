namespace SGH.Application.Models.JWT;

/// <summary>
/// Модель пользователя для JWT
/// </summary>
public class UserModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
}