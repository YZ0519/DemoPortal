using Microsoft.Extensions.Configuration;
using Shared.DTO;
using System.Data.SqlClient;
using System.Data;
using SimplePOSWeb.Services.Abstraction;
using Newtonsoft.Json;
using Shared.Helper;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SimplePOSWeb.Services.Implementation
{
    [Authorize]
    public class ProfileServices : IProfileServices
    {
        private readonly IHttpServices _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProfileServices> _logger;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public ProfileServices(IConfiguration configuration, ILogger<ProfileServices> logger, IHttpServices httpServices)
        {
            _configuration = configuration;
            _logger = logger;
            _httpService = httpServices;
        }
        public bool CreateUser(UserDto dto)
        {
            return false;
        }

        public async Task<UserDto> GetUser(AuthDto dto)
        {
            try
            {
                var httpClient = _httpService.GetHttpClientWithBearerToken();
                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(ConstantHelper.Profile.GetUser, httpContent);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserDto>(stringResponse);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new UserDto();
        }

        public async Task<bool> ChangePassword(AuthDto dto)
        {
            try
            {
                var httpClient = _httpService.GetHttpClientWithBearerToken();
                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(ConstantHelper.Profile.ChangePassword, httpContent);
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
    }
}
