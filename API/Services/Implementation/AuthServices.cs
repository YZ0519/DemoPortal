using API.Services.Abstraction;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO;
using Shared.Helper;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthServices> _logger;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public AuthServices(IConfiguration configuration, ILogger<AuthServices> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool Login(AuthDto dto)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                var sp = "[sp_Login]";
                return db.QueryFirstOrDefault<bool>(sp, new { dto.Username, dto.Password }, commandType: CommandType.StoredProcedure);
            }
        }
        public string GetSecretKey()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                var sp = "[sp_GetSecretKey]";
                var result = db.QueryFirstOrDefault<string>(sp, null, commandType: CommandType.StoredProcedure);
                if (result is not null)
                {
                    result = CryptoHelper.EncryptTextWithSecretKey(result, _configuration.GetSection("Auth:SecretKey").Value);
                }
                else
                {
                    result = string.Empty;
                }
                return result;
            }
        }
    }
}
