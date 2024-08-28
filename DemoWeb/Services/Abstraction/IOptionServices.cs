using Shared.DTO;

namespace SimplePOSWeb.Services.Abstraction
{
    public interface IOptionServices
    {
        Task<List<OptionSetDto>> GetOptionList();
    }
}
