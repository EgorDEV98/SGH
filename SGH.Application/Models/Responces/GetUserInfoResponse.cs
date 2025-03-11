namespace SGH.Application.Models.Responces;

public class GetUserInfoResponse
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Зашифрованный пароль
    /// </summary>
    public string Password { get; set; }
}