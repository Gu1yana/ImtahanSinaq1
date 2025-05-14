using Microsoft.AspNetCore.Mvc;

namespace ImtahanTest1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}