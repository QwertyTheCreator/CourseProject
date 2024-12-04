using Microsoft.IdentityModel.Tokens;
using Publications.Auth.Entities;
using Publications.Auth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Publications.Auth.Services;

public class TokenService
{
    public string GenerateToken(TokenGenerateModel tokenModel)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, tokenModel.Username),
            new Claim(ClaimTypes.NameIdentifier, tokenModel.Id.ToString()),
            new Claim(ClaimTypes.Role, tokenModel.RoleName)
        ];

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
