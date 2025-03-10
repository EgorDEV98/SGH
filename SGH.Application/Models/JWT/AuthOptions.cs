using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SGH.Application.Models.JWT;

public class AuthOptions
{
    /// <summary>
    /// Издатель токена
    /// </summary>
    public const string ISSUER = "SGH";
    
    /// <summary>
    /// Потрбитель токена
    /// </summary>
    public const string AUDIENCE = "SGH";
    
    /// <summary>
    /// Ключ шифрования
    /// </summary>
    const string KEY = "7LFfE$$aA5T5~zD3hkB5K4ltxuGiquZbn6at9OdZ1Xo34Eh1VwzVT7mUsiI2";
    
    /// <summary>
    /// Время жизни в минутах
    /// </summary>
    public const int LIFETIME = 7 * 24 * 60;
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}