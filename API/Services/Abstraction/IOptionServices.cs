using Shared.DTO;

namespace API.Services.Abstraction
{
    public interface IOptionServices
    {
        List<OptionSetDto> GetOptionList();
    }
}
