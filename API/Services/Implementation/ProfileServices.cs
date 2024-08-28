using API.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using Shared.DTO;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Transactions;
using System.Data.Common;

namespace API.Services.Implementation
{
    public class ProfileServices : IProfileServices
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProfileServices> _logger;
        private readonly string connectionString = "ConnectionStrings:ApplicationContext";
        public ProfileServices(IConfiguration configuration, ILogger<ProfileServices> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool CreateUser(UserDto dto)
        {
            return false;
        }
        public bool UpdateUser(UserDto dto)
        {
            bool isSuccess = true;
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected =
                        db.Execute("[sp_UpdateUser]",
                        new
                        {
                            dto.Username
                        },
                        commandType: CommandType.StoredProcedure, transaction: transaction);
                        isSuccess = (rowsAffected > 0);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }
        public UserDto GetUser(AuthDto dto)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                var sp = "[sp_GetUser]";
                return db.QueryFirstOrDefault<UserDto>(sp, new { dto.Username }, commandType: CommandType.StoredProcedure);
            }
        }
        public bool ChangePassword(AuthDto dto)
        {
            bool isSuccess = true;
            using (IDbConnection db = new SqlConnection(_configuration.GetSection(connectionString).Value))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = 
                        db.Execute("[sp_ChangePassword]",
                        new
                        {
                           dto.Username,dto.Password,dto.NewPassword
                        }, 
                        commandType: CommandType.StoredProcedure, transaction: transaction);
                        isSuccess = (rowsAffected > 0);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }
    }
}
