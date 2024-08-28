using Microsoft.Extensions.Configuration;
using Shared.DTO;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using API.Services.Abstraction;

namespace API.Services.Implementation
{
    public class OptionServices : IOptionServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OptionServices> _logger;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public OptionServices(IConfiguration configuration, ILogger<OptionServices> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public List<OptionSetDto> GetOptionList()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                var sp = "[sp_GetOptionList]";
                return db.Query<OptionSetDto>(sp, null, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
