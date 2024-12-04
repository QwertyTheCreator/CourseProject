using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Publications.Auth.Entities;

public class AuthOptions
{
    public const string ISSUER = "Publications.Auth"; // издатель токена
    public const string AUDIENCE = "Publications.Main"; // потребитель токена
    const string KEY = "supermegaamazingcoolpizdsecuritykey!_1_1";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
