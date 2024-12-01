using Microsoft.AspNetCore.Http;
using Publications.Main.Application.Abstractions.Services;
using System.Security.Claims;

namespace Publications.Main.Infrastructure.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public Guid? UserId => Guid.TryParse(GetClaim(ClaimTypes.NameIdentifier), out var id)
        ? id
        : null;

    private string? GetClaim(string type) => _httpContext.User.Claims.FirstOrDefault(c => c.Type == type)?.Value ?? null;

    private IEnumerable<string> GetClaims(string type) =>
        _httpContext.User.Claims
            .Where(c => c.Type == type)
            .Select(c => c.Value)
            .ToArray();
}
