using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PIN_Projekt.Models;

namespace PIN_Projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Predmet> Predmets { get; set; }

        public DbSet<Ispit> Ispits { get; set; }

        public DbSet<Smjer> Smjers { get; set; }

    }
}
