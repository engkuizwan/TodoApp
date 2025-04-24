using TodoApp.Model;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Work> Works{ get; set; }
    }
}
