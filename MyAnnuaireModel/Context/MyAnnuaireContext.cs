using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAnnuaireModel.Entities;

namespace MyAnnuaireModel.Context
{
    public class MyAnnuaireContext(DbContextOptions<MyAnnuaireContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Salarie> Salaries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Siege> Sieges { get; set; }
        public DbSet <TypeSiege> TypeSieges { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Visiteur> Visiteurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = "server=localhost;database=myannuaire;user=root; password=";
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }
    }
}
