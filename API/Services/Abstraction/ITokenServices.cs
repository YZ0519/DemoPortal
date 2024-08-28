using Shared.DTO;

namespace API.Services.Abstraction
{
    public interface ITokenServices
    {
        string GenerateAuthToken(AuthDto user);
    }
}
