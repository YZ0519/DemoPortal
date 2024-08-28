using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Shared.DTO;
using Shared.Helper;
using SimplePOSWeb.Services.Abstraction;

namespace SimplePOSWeb.Services.Implementation
{
    [Authorize]
    public class OptionServices : IOptionServices
    {
        private readonly IHttpServices _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OptionServices> _logger;
        public OptionServices(IConfiguration configuration, ILogger<OptionServices> logger, IHttpServices httpServices)
        {
            _configuration = configuration;
            _logger = logger;
            _httpService = httpServices;
        }
        public async Task<List<OptionSetDto>> GetOptionList()
        {
            try
            {
                var httpClient = _httpService.GetHttpClientWithBearerToken();
                var response = await httpClient.GetAsync(ConstantHelper.Option.GetOptionList);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<OptionSetDto>>(stringResponse);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new List<OptionSetDto>();
        }
    }
}
