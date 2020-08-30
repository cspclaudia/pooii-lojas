using Lojas.Models;
using Microsoft.EntityFrameworkCore;

namespace Lojas.Data
{
    public class LojasContext : DbContext
    {
        public LojasContext (DbContextOptions<LojasContext> options) : base (options) { }

        public DbSet<Entrega> Entrega { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Produto_Pedido> Produto_Pedido { get; set; }
    }
}