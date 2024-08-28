using Shared.DTO;

namespace SimplePOSWeb.Services.Abstraction
{
    public interface IAuthServices
    {
        Task<bool> Login(AuthDto dto);
        Task<string> GetSecretKey();
    }
}
