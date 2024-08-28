using API.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenServices _tokenServices ;
        public TokenController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }
        [Route("GenerateToken")]
        [HttpPost]
        public IActionResult GenerateToken(AuthDto user)
        {
            var token = _tokenServices.GenerateAuthToken(user);
            return Ok(token);
        }
    }
}
