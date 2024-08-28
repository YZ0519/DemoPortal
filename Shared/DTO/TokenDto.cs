using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class TokenDto
    {
        public string Value { get; set; }
        public long ExpiresIn { get; set; }
    }
}
