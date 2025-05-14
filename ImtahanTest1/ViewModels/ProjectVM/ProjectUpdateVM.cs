using ImtahanTest1.Models.Common;

namespace ImtahanTest1.ViewModels.ProjectVM
{
    public class ProjectUpdateVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath {  get; set; }  
    }
}