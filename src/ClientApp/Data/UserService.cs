using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientApp.Data;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenManagerService _tokenManager;

    public UserService(HttpClient httpClient, ITokenManagerService tokenManager)
    {
        _httpClient = httpClient;
        _tokenManager = tokenManager;
    }

    public async Task<LoginResponse?> Login(LoginModel login)
{
    var response = await _httpClient.PostAsJsonAsync("api/Users/login", login);
    response.EnsureSuccessStatusCode();

    if (response.Content.Headers.ContentType?.MediaType == "application/json")
    {
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (loginResponse != null)
        {
            // Update Authorization header with the new token
            _tokenManager.SetToken(loginResponse.AccessToken);
        }

        return loginResponse;
    }
    else
    {
        // Handle unexpected content type if necessary
        throw new InvalidOperationException("Unexpected content type received.");
    }
}

    public async Task<bool> Register(LoginModel register)
    {
        var jsonSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        var json = JsonSerializer.Serialize(register, jsonSettings);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsJsonAsync("api/Users/register", register);
        return response.IsSuccessStatusCode;
    }

}
