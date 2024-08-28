using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shared.DTO;
using System.Text;
using SimplePOSWeb.Services.Abstraction;
using Shared.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace SimplePOSWeb.Services.Implementation
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TokenServices> _logger;

        public TokenServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<TokenServices> logger)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> GenerateToken(AuthDto dto)
        {
            var tokenObj = await GenerateTokenFromApi(dto.Username, dto.Password);
            if (string.IsNullOrEmpty(tokenObj?.Value))
            {
                return false;
            }

            var Expires = DateTime.Now;
            if (_httpContextAccessor.HttpContext != null)
            {
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.GetSection("Jwt:TokenExpiryMinutes").Value));
                // create cookie option that expire in 2 hours
                CookieOptions option = new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = bool.Parse(_configuration.GetSection("IsHttps")?.Value ?? "False"),
                    Domain = _httpContextAccessor.HttpContext.Request.Host.Host,
                    Expires = Expires,
                    SameSite = SameSiteMode.Lax,
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append(ConstantHelper.Auth.UserWebTokenCookie, tokenObj.Value, option);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.Request?.Cookies == null)
                return string.Empty;

            return context.Request.Cookies[ConstantHelper.Auth.UserWebTokenCookie] == null ? string.Empty : context.Request.Cookies[ConstantHelper.Auth.UserWebTokenCookie];
        }

        public string GetTokenClaims(string key)
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var claimValue = jwt.Claims.FirstOrDefault(x => x.Type == key)?.Value ?? "";
            return claimValue;
        }
        public string GetUsername()
        {
            return GetTokenClaims("Username");
        }
        public string GetExpires()
        {
            return GetTokenClaims("Expires");
        }
        public bool IsAuthenticated()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo > DateTime.UtcNow;
        }

        private async Task<TokenDto> GenerateTokenFromApi(string userName, string password)
        {
            // get api implementation happens here
            // returns a token model
            try
            {
                AuthDto dto = new AuthDto()
                {
                    Username = userName,
                    Password = password
                };
                var handler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
                };
                HttpClient httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(GetApiConnectionString());

                //var httpClient = _httpService.GetHttpClientWithBearerToken();
                var json = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(ConstantHelper.Token.GenerateToken, httpContent);
                var stringResponse = await response.Content.ReadAsStringAsync();

                TokenDto token = new TokenDto()
                {
                    Value = stringResponse,
                    ExpiresIn = 315600000
                };
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        private string GetApiConnectionString()
        {
            var apiBaseUrl = _configuration.GetSection("ApiConnectionString:BaseUrl")?.Value?.ToString();
            return apiBaseUrl ?? string.Empty;
        }
    }
}
