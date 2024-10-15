using Microsoft.EntityFrameworkCore;
using RestAPICRUD.Models.Entity;

namespace RestAPICRUD.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
