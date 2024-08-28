using SimplePOSWeb.Services.Abstraction;

namespace SimplePOSWeb.Services.Implementation
{
    public class HttpServices : IHttpServices
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenService;

        private HttpClient? _httpClient;
        private HttpClient? _httpClientWithToken;
        const string scheme = "Bearer";

        public HttpServices(IConfiguration configuration, ITokenServices tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                var handler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
                };

                _httpClient = new HttpClient(handler);
                _httpClient.Timeout = TimeSpan.FromMinutes(5);
                _httpClient.BaseAddress = new Uri(GetApiConnectionString());

            }
            return _httpClient;
        }

        public HttpClient GetHttpClientWithBearerToken()
        {
            var token = _tokenService.GetToken()?.ToString();

            if (_httpClientWithToken == null)
            {
                var handler = new SocketsHttpHandler
                {
                    //ref https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
                    PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
                };

                _httpClientWithToken = new HttpClient(handler);
                _httpClientWithToken.Timeout = TimeSpan.FromMinutes(5);
                _httpClientWithToken.BaseAddress = new Uri(GetApiConnectionString());

                _httpClientWithToken.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme, token);
            }
            else if (!string.IsNullOrEmpty(token))
            {
                if (_httpClientWithToken.DefaultRequestHeaders.Authorization?.Parameter != token)
                {
                    _httpClientWithToken.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme, token);
                }
            }

            return _httpClientWithToken;
        }


        private string GetApiConnectionString()
        {
            var apiBaseUrl = _configuration.GetSection("ApiConnectionString:BaseUrl")?.Value?.ToString();
            return apiBaseUrl ?? string.Empty;
        }

    }
}
