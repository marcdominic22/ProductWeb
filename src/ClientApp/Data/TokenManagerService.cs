using System.Net.Http.Headers;

namespace ClientApp.Data;

public interface ITokenManagerService
{
    string GetToken();
    void SetToken(string token);
}

public class TokenManagerService : ITokenManagerService
{
    private string _token;
    private readonly HttpClient _httpClient;

    public TokenManagerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _token = string.Empty; // Initialize with an empty token or null
    }

    public string GetToken()
    {
        // Implement logic to retrieve or refresh token as needed
        return _token;
    }

    public void SetToken(string token)
    {
        _token = token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}