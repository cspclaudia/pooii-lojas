using Microsoft.EntityFrameworkCore;
using Lojas.Models;

namespace Lojas.Data
{
    public class LojasContext : DbContext
    {
        public LojasContext (DbContextOptions<LojasContext> options)
            : base(options)
        {
        }

        public DbSet<Loja> Loja { get; set; }
    }
}