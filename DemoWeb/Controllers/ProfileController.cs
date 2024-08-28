using AutoMapper;
using SimplePOSWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.DTO;
using Shared.Helper;
using System.Security.Cryptography.X509Certificates;
using SimplePOSWeb.Helper;
using SimplePOSWeb.Process.Abstraction;
using SimplePOSWeb.Services.Abstraction;
using SimplePOSWeb.ViewModel.OptionSet;
using SimplePOSWeb.ViewModel.Profile;

namespace SimplePOSWeb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ITokenServices _tokenServices;
        private readonly IProfileServices _profileServices;
        private readonly IAuthServices _authServices;
        private readonly IOptionProcess _optionProcess;
        private readonly string _username;

        public ProfileController(ILogger<ProfileController> logger, IConfiguration config,IMapper mapper,ITokenServices tokenServices,
            IProfileServices profileServices, IAuthServices authServices, IOptionProcess optionProcess)
        {
            _logger = logger;
            _config = config;
            _mapper = mapper;
            _tokenServices = tokenServices;
            _profileServices = profileServices;
            _authServices = authServices;
            _optionProcess = optionProcess;
            _username = _tokenServices.GetUsername();
        }

        public async Task<IActionResult> Index()
        {
            AuthDto loginDto = new AuthDto();
            loginDto.Username = _username;
            var userProfileTask = _profileServices.GetUser(loginDto);
            var optionListTask = _optionProcess.OptionConfiguration();
            List<Task> listTask = [userProfileTask, optionListTask];
            await Task.WhenAll(listTask);

            var userProfile = await userProfileTask;
            var optionList = await optionListTask;

            var vm = _mapper.Map<UserVM>(userProfile);
            vm.optionDDL = optionList;
            return View(vm);
        }
        public async Task<IActionResult> ChangePassword(UserVM vm) {
            return RedirectToAction("Index", "Profile");
        }
    }
}
