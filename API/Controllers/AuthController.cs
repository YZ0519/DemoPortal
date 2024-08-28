using API.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Helper;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices; 
        }
        
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AuthDto user)
        {
            var login = _authServices.Login(user);//admin password:BOZ17AwPWCPcBNetkp1c+A==
            return Ok(login);
        }
        [Route("GetSecretKey")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetSecretKey()
        {
            var result = _authServices.GetSecretKey();
            return Ok(result);
        }
    }
}
