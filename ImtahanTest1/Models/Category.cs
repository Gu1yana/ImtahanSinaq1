using ImtahanTest1.Models.Common;

namespace ImtahanTest1.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Project>? Projects {  get; set; }    
    }
}