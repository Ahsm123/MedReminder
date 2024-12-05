using MedReminder.Shared.DTOs;

namespace MedReminder.ApiClient;

public interface IApiClient
{
    Task<T> GetAsync<T>(string endpoint, string? token = null);
    Task<T> PostAsync<T>(string endpoint, object payload, string? token = null);
    Task DeleteAsync(string endpoint, string? token = null);
    Task<T> PutAsync<T>(string endpoint, object payload, string? token = null);
    Task<LoginResponseDTO> RefreshTokenAsync(string refreshToken);
}

