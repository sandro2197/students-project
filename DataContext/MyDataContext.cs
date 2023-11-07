using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;

namespace WebApplication3.DataContext
{
    public class MyDataContext : DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options)
        : base(options) { }

        public DbSet<Student> Students => Set<Student>();
    }
}
