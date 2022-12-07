using CabSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CabSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cab> Cabs { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Book> Books { get; set; }


    }
}
