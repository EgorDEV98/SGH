using System.Security.Cryptography;
using System.Text;

namespace SGH.Application.Common;

public static class DataEncryptor
{
    public static string Encrypt(string plainText)
    {
        if (plainText == null)
        {
            throw new ArgumentNullException("Plain text is null");
        }

        var provider = new HMACSHA256();
        var encoding = new UnicodeEncoding();
        var encryptedBytes = provider.ComputeHash(encoding.GetBytes(plainText));

        return Convert.ToBase64String(encryptedBytes);
    }
}