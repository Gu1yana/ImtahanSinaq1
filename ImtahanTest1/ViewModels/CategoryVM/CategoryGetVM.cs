using ImtahanTest1.Models;
using ImtahanTest1.Models.Common;

namespace ImtahanTest1.ViewModels.CategoryVM
{
    public class CategoryGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}