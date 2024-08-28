using Microsoft.AspNetCore.Mvc;

namespace SimplePOSWeb.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
