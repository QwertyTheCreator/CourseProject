using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Publications.Main.Domain.Constants;

public static class JwtOptions
{
    public const string ISSUER = "Publications.Auth"; // издатель токена
    public const string AUDIENCE = "Publications.Main"; // потребитель токена
    const string KEY = "supermegaamazingcoolpizdsecuritykey!_1_1";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
