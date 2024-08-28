using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Helper;
using SimplePOSWeb.Services.Abstraction;
using SimplePOSWeb.ViewModel.Auth;

namespace SimplePOSWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;
        private readonly IAuthServices _authServices;
        private readonly IConfiguration _configuration;

        public AuthController(IMapper mapper,IAuthServices authServices,ITokenServices tokenServices, IConfiguration configuration)
        {
            _tokenServices = tokenServices;
            _authServices = authServices;
            _configuration = configuration;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            if (_tokenServices.IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task<IActionResult> Login(LoginVM vm)
        {
            string secretKey = await _authServices.GetSecretKey();
            secretKey = CryptoHelper.DecryptTextWithSecretKey(secretKey, _configuration.GetSection("Auth:SecretKey").Value);
            vm.Password = CryptoHelper.EncryptTextWithSecretKey(vm.Password, secretKey);
            var dto = _mapper.Map<AuthDto>(vm);

            var loginTask = await _authServices.Login(dto);
            if (loginTask)
            {
                bool token = await _tokenServices.GenerateToken(dto);
                if (token)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //Show Message Fail
                @TempData["ErrorMsg"] = "Login Failed. Please try again!";
            }
            return RedirectToAction("Index", "Auth");
        }

        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
