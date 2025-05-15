using ImtahanTest1.DataAccessLayer;
using ImtahanTest1.Models;
using ImtahanTest1.ViewModels.CategoryVM;
using ImtahanTest1.ViewModels.ProjectVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(KlinikDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Categories.Select(x => new CategoryGetVM
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return View(datas);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            Category category = new();
            category.Name = vm.Name;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            int result = await _context.Categories.Where(x => x.Id == id)
                  .ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Categories.
         Select(x => new CategoryUpdateVM { Id = x.Id, Name=x.Name}).FirstOrDefaultAsync(x =>x.Id==id);
            if (entity is null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return BadRequest();
            entity.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}