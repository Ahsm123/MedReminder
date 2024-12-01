namespace MedReminder.Web.Service.Interfaces;

public interface ICookieService
{
    string GetJwtToken();
    void SetJwtToken(string token, TimeSpan expirationTime);
    void DeleteJwtToken();
    int? ExstractUserIdFromToken();
}
