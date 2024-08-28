using API.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OptionController : Controller
    {
        private readonly IOptionServices _optionServices;
        public OptionController(IOptionServices optionServices)
        {
            _optionServices = optionServices;
        }
        [Route("GetOptionList")]
        [HttpGet]
        public IActionResult GetOptionList()
        {
            var login = _optionServices.GetOptionList();
            return Ok(login);
        }
    }
}
