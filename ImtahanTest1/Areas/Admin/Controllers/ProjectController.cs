using ImtahanTest1.DataAccessLayer;
using ImtahanTest1.ViewModels.CategoryVM;
using ImtahanTest1.ViewModels.ProjectVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Areas.Admin.Controllers
{
    public class ProjectController(KlinikDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
           var projects= await _context.Projects.Select(x=>new ProjectGetVM
           {
               Id=x.Id,
               Content=x.Content,
               CategoryName=x.Category.Name,
               ImagePath=x.ImagePath
           }).ToListAsync();
            return View(projects);
        }
        private async Task ViewBags()
        {
            var categories = await _context.Categories.Select(
                x => new CategoryGetVM
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

            ViewBag.Categories = categories;
        }
        public async Task<IActionResult> Create()
        {
            await ViewBags();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateVM vm)
        {
            if(vm.ImageFile is not null)
            {
                if(!vm.ImageFile.IsValidSize())
            }


            if (!ModelState.IsValid)
            {
                await ViewBags();
                return View(vm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
