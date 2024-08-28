using AutoMapper;
using Shared.DTO;
using Shared.Helper;
using SimplePOSWeb.Process.Abstraction;
using SimplePOSWeb.Services.Abstraction;
using SimplePOSWeb.ViewModel.OptionSet;

namespace SimplePOSWeb.Process.Implementation
{
    public class OptionProcess : IOptionProcess
    {
        private readonly ILogger<OptionProcess> _logger;
        private readonly IOptionServices _optionServices;
        private readonly IMapper _mapper;
        public OptionProcess(ILogger<OptionProcess> logger, IMapper mapper, IOptionServices optionServices) {
            _optionServices = optionServices;
            _mapper = mapper;
        }
        public async Task<OptionConfigurationVM> OptionConfiguration() {
            var optionListTask = _optionServices.GetOptionList();
            await Task.WhenAll(optionListTask);
            var optionList = await optionListTask;
            var optionDDL = _mapper.Map<List<OptionSetDto>, List<OptionSetVM>>(optionList);
            OptionConfigurationVM optionConfiguration = new OptionConfigurationVM();
            optionConfiguration.sampleDDL = optionDDL.Where(m => m.Type == DDLHelper.Option.Sample).ToList();
            return optionConfiguration;
        }
    }
}
