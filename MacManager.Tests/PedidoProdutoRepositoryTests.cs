using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using MacManager.Infra.Data;
using MacManager.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MacManager.Tests
{
    public class PedidoProdutoRepositoryTests
    {
        private readonly MacManagerContext _context;

        public PedidoProdutoRepositoryTests()
        {
            // Usando InMemoryDatabase para simular um banco de dados real
            var options = new DbContextOptionsBuilder<MacManagerContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new MacManagerContext(options);
        }

        [Fact]
        public async Task ObterPedidosPorAreaCozinhaAsync_DeveRetornarPedidosFiltradosPorAreaCozinha()
        {
            // Arrange
            var pedidoProduto1 = new PedidoProduto
            {
                Produto = new Produto
                {
                    Nome = "Fritos Test",
                    AreaCozinha = AreaCozinha.Fritos
                },
                Quantidade = 1,
                PedidoId = 1,
                Pedido = new Pedido { Id = 1 } // Adicionando Pedido corretamente
            };
            var pedidoProduto2 = new PedidoProduto
            {
                Produto = new Produto
                {
                    Nome = "Grelhados Test",
                    AreaCozinha = AreaCozinha.Grelhados
                },
                Quantidade = 2,
                PedidoId = 2,
                Pedido = new Pedido { Id = 2 } // Adicionando Pedido corretamente
            };
            var pedidoProduto3 = new PedidoProduto
            {
                Produto = new Produto
                {
                    Nome = "Frango Frito",
                    AreaCozinha = AreaCozinha.Fritos
                },
                Quantidade = 3,
                PedidoId = 3,
                Pedido = new Pedido { Id = 3 } // Adicionando Pedido corretamente
            };

            // Adicionando os dados no contexto (banco de dados simulado)
            _context.PedidoProdutos.AddRange(pedidoProduto1, pedidoProduto2, pedidoProduto3);
            await _context.SaveChangesAsync(); // Persistindo os dados

            // Verificando se os dados estão na memória
            Assert.Equal(3, _context.PedidoProdutos.Count()); // Verifica se os 3 registros foram inseridos corretamente

            // Act
            var repository = new PedidoProdutoRepository(_context);  // Usando o contexto real no repositório
            var result = await repository.ObterPedidosPorAreaCozinhaAsync(AreaCozinha.Fritos);

            // Assert
            Assert.Equal(2, result.Count()); // Espera-se que apenas os pedidos 1 e 3 (da área Fritos) sejam retornados
            Assert.All(result, pp => Assert.Equal(AreaCozinha.Fritos, pp.Produto.AreaCozinha)); // Verifica se todos os produtos retornados são da área 'Fritos'
        }
    }
}
