using MedReminder.Shared.DTOs;
using RestSharp;

namespace MedReminder.ApiClient;

public class ApiClient : IApiClient
{
    private readonly RestClient _restClient;

    public ApiClient(string baseUrl)
    {
        _restClient = new RestClient(baseUrl);
    }

    public async Task<T> GetAsync<T>(string endpoint, string? token = null)
    {
        var request = new RestRequest(endpoint, Method.Get);
        AddAuthorizationHeader(request, token);

        var response = await _restClient.ExecuteAsync<T>(request);
        if (!response.IsSuccessful)
            throw new Exception(response.Content ?? "API call failed");

        return response.Data!;
    }

    public async Task<T> PostAsync<T>(string endpoint, object payload, string token = null)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(payload);
        AddAuthorizationHeader(request, token);

        var response = await _restClient.ExecuteAsync<T>(request);

        if (!response.IsSuccessful)
        {
            throw new Exception(response.Content ?? "API call failed");
        }

        return response.Data!;
    }

    public async Task DeleteAsync(string endpoint, string? token = null)
    {
        var request = new RestRequest(endpoint, Method.Delete);
        AddAuthorizationHeader(request, token);

        var response = await _restClient.ExecuteAsync(request);
        if (!response.IsSuccessful)
            throw new Exception(response.Content ?? "API call failed");
    }

    public async Task<T> PutAsync<T>(string endpoint, object payload, string? token = null)
    {
        var request = new RestRequest(endpoint, Method.Put);
        request.AddJsonBody(payload);
        AddAuthorizationHeader(request, token);

        var response = await _restClient.ExecuteAsync<T>(request);
        if (!response.IsSuccessful)
            throw new Exception(response.Content ?? "API call failed");

        return response.Data!;
    }

    private void AddAuthorizationHeader(RestRequest request, string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }
    }

    public async Task<LoginResponseDTO> RefreshTokenAsync(string refreshToken)
    {
        var request = new RestRequest("Auth/RefreshToken", Method.Post);
        request.AddJsonBody(new { RefreshToken = refreshToken });

        var response = await _restClient.ExecuteAsync<LoginResponseDTO>(request);

        if (!response.IsSuccessful)
            throw new Exception(response.Content ?? "Failed to refresh token");

        return response.Data!;
    }

}
