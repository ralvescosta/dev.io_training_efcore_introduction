using System;
using System.Linq;
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
          .UseNpgsql("Host=localhost;Database=EF;Username=postgres;Password=123456;", 
            p=> p.EnableRetryOnFailure(
                     maxRetryCount: 2, 
                     maxRetryDelay: TimeSpan.FromSeconds(5), 
                     errorCodesToAdd: null
            ).MigrationsHistoryTable("curso_ef_core"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        MapearPropriedadesEsquecidas(modelBuilder);
    }

    private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p=>p.ClrType == typeof(string));

                foreach(var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                        {
                            //property.SetMaxLength(100);
                            property.SetColumnType("VARCHAR(100)");
                        }
                }
            }
        }
  }
}