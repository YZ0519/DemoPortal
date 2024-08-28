using Shared.DTO;

namespace API.Services.Abstraction
{
    public interface IAuthServices
    {
        bool Login(AuthDto user);
        string GetSecretKey();
    }
}
