using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext() : base()
    {
    }

    DbSet<Pedido> Pedidos { get; set; }
    DbSet<Produto> Produtos { get; set; }
    DbSet<PedidoItem> PedidoItens { get; set; }
    DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=EF;Username=postgres;Password=123456;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
  }
}