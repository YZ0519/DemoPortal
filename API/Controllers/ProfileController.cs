using API.Services.Abstraction;
using API.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Helper;

namespace API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileServices _profileServices;
        public ProfileController(IProfileServices profileServices)
        {
            _profileServices = profileServices; 
        }
        
        [Route("GetUser")]
        [HttpPost]
        public IActionResult GetUser(AuthDto user)
        {
            var login = _profileServices.GetUser(user);
            return Ok(login);
        }
        [Route("UpdateUser")]
        [HttpPost]
        public IActionResult UpdateUser(UserDto user)
        {
            var login = _profileServices.UpdateUser(user);
            return Ok(login);
        }
        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(AuthDto user)
        {
            var login = _profileServices.ChangePassword(user);
            return Ok(login);
        }
    }
}
