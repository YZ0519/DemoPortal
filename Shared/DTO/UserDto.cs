using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public string LastLoginDate { get; set; } = string.Empty;
    }
}
