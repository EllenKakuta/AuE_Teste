using AuE_Teste.Models;
using Microsoft.EntityFrameworkCore;

namespace AuE_Teste.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EstatisticasViewModel> Estatisticas { get; set; }
    }
}
