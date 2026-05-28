using System.Net.Http.Json;

namespace TradeXPro.Services;

/// <summary>
/// HttpClient service for calling TradeXPro.API.
/// Web MVC NEVER connects to database directly - only through this service.
/// </summary>
public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Auth
    public async Task<T?> PostAsync<T>(string endpoint, object data) where T : class
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> GetAsync<T>(string endpoint) where T : class
    {
        return await _httpClient.GetFromJsonAsync<T>(endpoint);
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data) where T : class
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
