using ImtahanTest1.Models;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.DataAccessLayer
{
    public class KlinikDbContext:DbContext
    {
        public KlinikDbContext(DbContextOptions opt) : base(opt) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}