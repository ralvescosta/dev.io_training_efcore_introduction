using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.Data
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext() : base()
    {
    }

    private static readonly ILoggerFactory _logger = LoggerFactory.Create(l => l.AddConsole());

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
          .UseLoggerFactory(_logger)
          .EnableSensitiveDataLogging()
          .UseNpgsql("Host=localhost;Database=EF;Username=postgres;Password=123456;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
  }
}