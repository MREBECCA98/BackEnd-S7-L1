using Microsoft.EntityFrameworkCore;


namespace BackEnd_S7_L1.Models.Entities
{
    public class ApplicationDbContext : DbContext

    {
        //tabella Students
        public DbSet<Student> Students { get; set; }
        //costruttore
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
}
