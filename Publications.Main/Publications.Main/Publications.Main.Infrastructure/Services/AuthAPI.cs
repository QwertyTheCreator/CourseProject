using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Infrastructure.Configuration;

namespace Publications.Main.Infrastructure.Services;

public class AuthAPI(InfrastructureConfiguration infrastructureConfiguration) : IAuthAPI
{
    private readonly string _baseUrl = infrastructureConfiguration.AuthServiceUrl;

    public string LoginUrl => $"{_baseUrl}token/login";
    public string UserUrl => $"{_baseUrl}user";
}
