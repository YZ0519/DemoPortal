using SimplePOSWeb.ViewModel.OptionSet;

namespace SimplePOSWeb.Process.Abstraction
{
    public interface IOptionProcess
    {
        public Task<OptionConfigurationVM> OptionConfiguration();
    }
}
