using Microsoft.AspNetCore.Mvc;

namespace ImtahanTest1.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
