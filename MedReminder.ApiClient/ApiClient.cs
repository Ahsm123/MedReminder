using RestSharp;

namespace MedReminder.ApiClient;

public class ApiClient : IApiClient
{
    private readonly RestClient _restClient;

    public ApiClient(string baseUrl)
    {
        _restClient = new RestClient(baseUrl);
    }
    public async Task<RestResponse> DeleteAsync(string endpoint, string token = null)
    {
        var request = new RestRequest(endpoint, Method.Delete);
        AddAuthorizationHeader(request, token);
        return await _restClient.ExecuteAsync(request);

    }

    public async Task<RestResponse<T>> GetAsync<T>(string endpoint, string token = null)
    {
        var request = new RestRequest(endpoint, Method.Get);
        AddAuthorizationHeader(request, token);
        return await _restClient.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse<T>> PostAsync<T>(string endpoint, object payload, string token = null)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(payload);
        AddAuthorizationHeader(request, token);
        return await _restClient.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse> PutAsync<T>(string endpoint, object payload, string token = null)
    {
        var request = new RestRequest(endpoint, Method.Put);
        request.AddJsonBody(payload);
        AddAuthorizationHeader(request, token);
        return await _restClient.ExecuteAsync(request);
    }

    private void AddAuthorizationHeader(RestRequest request, string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}
