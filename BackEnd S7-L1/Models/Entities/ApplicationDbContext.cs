using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BackEnd_S7_L1.Models.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>


    {
        //costruttore
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //tabella Students
        public DbSet<Student> Students { get; set; }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }



    }
}
