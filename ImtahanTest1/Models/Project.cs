using ImtahanTest1.Models.Common;

namespace ImtahanTest1.Models
{
    public class Project:BaseEntity 
    {
        public string Content {  get; set; }  
        public string ImagePath { get; set; }
        public Category Category { get; set; }
        public int CategoryId {  get; set; }
    }
}