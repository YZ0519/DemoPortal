using Newtonsoft.Json;
using Shared.DTO;
using Shared.Helper;
using System.ComponentModel.Design;
using System.Text;
using SimplePOSWeb.Services.Abstraction;

namespace SimplePOSWeb.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly IHttpServices _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthServices> _logger;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public AuthServices(IConfiguration configuration, ILogger<AuthServices> logger,IHttpServices httpServices)
        {
            _configuration = configuration;
            _logger = logger;
            _httpService = httpServices;
        }
        public async Task<bool> Login(AuthDto dto)
        {
            try
            {
                var httpClient = _httpService.GetHttpClient();
                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(ConstantHelper.Auth.Login, httpContent);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(stringResponse);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<string> GetSecretKey()
        {
            try
            {
                var httpClient = _httpService.GetHttpClient();
                var response = await httpClient.GetAsync(ConstantHelper.Auth.GetSecretKey);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = stringResponse;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return string.Empty;
        }
    }
}
