using GestaodeBiblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaodeBiblioteca.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
 
        public DbSet<UsuarioDB> Usuario { get; set; }

        public DbSet<LivroDB> Livro { get; set; }

        public DbSet<EmprestimoDB> Emprestimo { get; set; }

    }
}
