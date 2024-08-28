using Shared.DTO;

namespace API.Services.Abstraction
{
    public interface IProfileServices
    {
        bool CreateUser(UserDto dto);
        bool UpdateUser(UserDto dto);
        UserDto GetUser(AuthDto dto);
        bool ChangePassword(AuthDto dto);
    }
}
