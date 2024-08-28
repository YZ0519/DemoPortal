using API.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO;
using Shared.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implementation
{
    public class TokenServices : ITokenServices
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenServices> _logger;
        private readonly IAuthServices _authServices;
        private readonly IProfileServices _profileServices;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public TokenServices(IConfiguration configuration, ILogger<TokenServices> logger,IAuthServices authServices,IProfileServices profileServices)
        {
            _configuration = configuration;
            _logger = logger;
            _authServices = authServices;
            _profileServices = profileServices;
        }
        public string GenerateAuthToken(AuthDto dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var Expires = DateTime.Now;
            if (_authServices.Login(dto))
            {
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.GetSection("Jwt:TokenExpiryMinutes").Value));
                var userProfile = _profileServices.GetUser(dto);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new("Username", userProfile.Username),
                    new("AccessLevel", userProfile.AccessLevel),
                    new("Expires",Expires.ToString("yyyy/MM/dd HH:mm:ss"))
                    }),
                    IssuedAt = DateTime.Now,
                    Expires = Expires,
                    Audience = _configuration.GetSection("Jwt:Audience").Value,
                    Issuer = _configuration.GetSection("Jwt:Issuer").Value,
                    SigningCredentials = creds,
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var result = tokenHandler.WriteToken(token);

                return result;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
