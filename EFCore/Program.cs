using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Data;
using EFCore.Domain;
using EFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoverRegistro();
        }

        private static async Task InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto",
                CodigoBarra = "123456789123",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new ApplicationContext();

            await db.Produtos.AddAsync(produto);
            var resultado = await db.SaveChangesAsync();
            Console.WriteLine($"Quantidade de registro(s): {resultado}");
        }

        private static async Task InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto",
                CodigoBarra = "123456789123",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Meu Nome",
                CEP = "Meu CEP",
                Cidade = "Minha Cidade",
                Estado =  "Meu Estado",
                Telefone = "99990100"
            };

            using var db = new ApplicationContext();

            await db.AddRangeAsync(produto, cliente);
            var resultado = await db.SaveChangesAsync();
            Console.WriteLine($"Quantidade de registro(s): {resultado}");
        }
    
        private static void ConsultarDadosComTracking()
        {
            using var db = new ApplicationContext();

            var consulta = db.Clientes
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Id)
                .ToList();
            foreach (var cliente in consulta)
            {
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void ConsultarDadosSemTracking()
        {
            using var db = new ApplicationContext();

            var consulta = db.Clientes
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Id)
                .AsNoTracking()
                .ToList();
            foreach (var cliente in consulta)
            {
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                CriadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                 {
                     new PedidoItem
                     {
                         ProdutoId = produto.Id,
                         Desconto = 0,
                         Quantidade = 1,
                         Valor = 10,
                     }
                 }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }
        
        private static void CarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void AtualizarDados()
        {
            using var db = new ApplicationContext();
            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new 
            {
                Nome = "Cliente Desconectado Passo 3",
                Telefone = "7966669999"
            };
            
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 3};
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }
    }
}
