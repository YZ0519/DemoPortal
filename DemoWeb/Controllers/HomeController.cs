using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimplePOSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using SimplePOSWeb.Services.Abstraction;
using Shared.DTO;
namespace SimplePOSWeb.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly ITokenServices _tokenServices;
    private readonly IAuthServices _authServices;
    private readonly string _username;
    public HomeController(ILogger<HomeController> logger, IConfiguration config, IMapper mapper, ITokenServices tokenServices)
    {
        _logger = logger;
        _config = config;
        _mapper = mapper;
        _tokenServices = tokenServices;
        _username = _tokenServices.GetUsername();
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult About()
    {
        return View();
    }
}
