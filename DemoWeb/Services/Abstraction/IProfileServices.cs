using Shared.DTO;

namespace SimplePOSWeb.Services.Abstraction
{
    public interface IProfileServices
    {
        bool CreateUser(UserDto dto);
        Task<UserDto> GetUser(AuthDto dto);
        Task<bool> ChangePassword(AuthDto dto);
    }
}
