using Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Empleado> Empleados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder); 
        }
    }
}