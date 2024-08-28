using Shared.DTO;

namespace SimplePOSWeb.Services.Abstraction
{
    public interface ITokenServices
    {
        Task<bool> GenerateToken(AuthDto dto);

        string GetToken();
        string GetTokenClaims(string key);
        string GetUsername();
        string GetExpires();
        bool IsAuthenticated();
    }
}
