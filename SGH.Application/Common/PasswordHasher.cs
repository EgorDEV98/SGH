using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SGH.Application.Common;

public static class PasswordHasher
{
    private const int SaltSize = 16; // Размер соли в байтах
    private const int KeySize = 32;  // Размер хэша в байтах
    private const int Iterations = 100000; // Количество итераций

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, Iterations, KeySize);
        
        return Convert.ToBase64String(Combine(salt, hash));
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, Iterations, KeySize);
        
        for (int i = 0; i < KeySize; i++)
        {
            if (hashBytes[SaltSize + i] != hash[i])
                return false;
        }
        return true;
    }
    
    private static byte[] Combine(byte[] salt, byte[] hash)
    {
        byte[] combined = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, combined, salt.Length, hash.Length);
        return combined;
    }
}