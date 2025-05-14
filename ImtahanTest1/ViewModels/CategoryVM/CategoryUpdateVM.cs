using ImtahanTest1.Models;
using ImtahanTest1.Models.Common;

namespace ImtahanTest1.ViewModels.CategoryVM
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}