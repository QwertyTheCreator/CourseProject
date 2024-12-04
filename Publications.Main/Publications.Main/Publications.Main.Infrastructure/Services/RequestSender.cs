using Newtonsoft.Json;
using Publications.Main.Application.Abstractions.Services;
using Publications.Main.Application.Models;
using Publications.Main.Application.Models.AuthApi;
using Publications.Main.Domain;
using System.Text;

namespace Publications.Main.Infrastructure.Services;

public class RequestSender(IHttpClientFactory httpClientFactory, IAuthAPI authAPI) : IRequestSender
{
    public async Task<Result<string>> CreateUser(CreateUserDto createUserDto)
    {
        using var httpClient = httpClientFactory.CreateClient();

        using StringContent stringContent = new(JsonConvert.SerializeObject(createUserDto),
            Encoding.UTF8,
            "application/json");

        Console.WriteLine(JsonConvert.SerializeObject(createUserDto));

        var result = await httpClient.PostAsync(authAPI.UserUrl, stringContent);
        if (!result.IsSuccessStatusCode)
        {
            return Result<string>.Failure(Error.Conflict("Ошибка при запросе к сервису авторизации"));
        }

        var jwt = await result.Content.ReadAsStringAsync();

        return Result<string>.Success(jwt);
    }
}
