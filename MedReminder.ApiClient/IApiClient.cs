using RestSharp;

namespace MedReminder.ApiClient;

public interface IApiClient
{
    Task<RestResponse<T>> PostAsync<T>(string endpoint, object payload, string token = null);
    Task<RestResponse<T>> GetAsync<T>(string endpoint, string token = null);
    Task<RestResponse> DeleteAsync(string endpoint, string token = null);
    Task<RestResponse> PutAsync<T>(string endpoint, object payload, string token = null);

}
