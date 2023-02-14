using Microsoft.EntityFrameworkCore;

namespace pumafuelbar.Models
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext>options):base(options) { 
        
        }
        public DbSet<price>prices { get; set; }
    }
}
