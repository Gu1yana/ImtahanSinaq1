using ImtahanTest1.DataAccessLayer;
using ImtahanTest1.Extensions;
using ImtahanTest1.Models;
using ImtahanTest1.ViewModels.CategoryVM;
using ImtahanTest1.ViewModels.ProjectVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                if(!vm.ImageFile.IsValidSize(20))
                    ModelState.AddModelError("ImageFile", "Fayl olchusu 20kb-dan cox olmamalidir!");
                if (!vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Faylin tipi 'image' olmalidir");
            }
            if(!ModelState.IsValid)
            {
                await ViewBags();
                return View(vm);
            }
            Project project = new Project()
            {
                Content = vm.Content,
                CategoryId = vm.CategoryId,
                ImagePath = await vm.ImageFile!.UploadAsync(Path.Combine("wwwroot", "imgs", "projects")),
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int?id)
        {
            if(!id.HasValue || id<1) return BadRequest();
            int result = await _context.Projects.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0) return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int?id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var projects = await _context.Projects.Select(x => new ProjectUpdateVM
            {
                Id = x.Id,
                Content = x.Content,
                CategoryId = x.CategoryId,  
                ImagePath = x.ImagePath,
            }).ToListAsync();
            if (!ModelState.IsValid)
                return NotFound();

           return View(projects);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int?id, ProjectUpdateVM vm)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            if(!ModelState.IsValid) return View(vm);
            var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();
            if(vm.ImageFile is not  null)
            {
                string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.ImageFile.FileName);
                string path=Path.Combine("wwwroot","imgs","project", newFileName);
                await using(FileStream fs=System.IO.File.Create(path))
                { await vm.ImageFile.CopyToAsync(fs); }
                entity.ImagePath = newFileName;
                entity.CategoryId = vm.CategoryId;  
                entity.Content = vm.Content;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
    }
}
