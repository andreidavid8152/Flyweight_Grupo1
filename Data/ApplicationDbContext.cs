using Flyweight_Grupo1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flyweight_Grupo1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Autor-Libro
            modelBuilder.Entity<Autor>()
                .HasMany(a => a.Libros)
                .WithOne(l => l.Autor)
                .HasForeignKey(l => l.AutorId);

            // Configuración de la relación Genero-Libro
            modelBuilder.Entity<Genero>()
                .HasMany(g => g.Libros)
                .WithOne(l => l.Genero)
                .HasForeignKey(l => l.GeneroId);
        }
    }
}
