using System.Security.Cryptography;
using System.Text;

namespace Publications.Auth.Services;

public class HashService
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Преобразуем пароль в байты
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Хэшируем пароль
            byte[] hash = sha256.ComputeHash(bytes);

            // Преобразуем хэш в строку в формате Base64
            return Convert.ToBase64String(hash);
        }
    }
}
