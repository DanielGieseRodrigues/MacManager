using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos;
using MacManager.Application.UseCases.Pedidos.ListarPedido;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using Moq;

namespace MacManager.Tests
{
    public class ListarPedidosPorAreaUseCaseTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<IPedidoProdutoRepository> _pedidoProdutoRepositoryMock;
        private readonly ListarPedidosPorAreaUseCase _useCase;

        public ListarPedidosPorAreaUseCaseTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _pedidoProdutoRepositoryMock = new Mock<IPedidoProdutoRepository>();
            _useCase = new ListarPedidosPorAreaUseCase(_pedidoRepositoryMock.Object, _pedidoProdutoRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarPedidosFiltradosPorAreaCozinha()
        {
            // Arrange
            var areaCozinha = AreaCozinha.Fritos;

            //Cria os produtos e pedidos para o teste unitario
            var pedidoProduto1 = new PedidoProduto
            {
                Produto = new Produto { Nome = "Frango Frito Test", AreaCozinha = AreaCozinha.Fritos },
                Pedido = new Pedido { Id = 1 },
                PedidoId = 1,
                Quantidade = 1
            };
            var pedidoProduto2 = new PedidoProduto
            {
                Produto = new Produto { Nome = "Ovo Frito Test", AreaCozinha = AreaCozinha.Fritos },
                PedidoId = 2,
                Pedido = new Pedido { Id = 2 },
                Quantidade = 2
            };
            var pedidoProduto3 = new PedidoProduto
            {
                Produto = new Produto { Nome = "Saladas Test", AreaCozinha = AreaCozinha.Saladas },
                PedidoId = 3,
                Pedido = new Pedido { Id = 3 },
                Quantidade = 1
            };

            _pedidoProdutoRepositoryMock
                .Setup(repo => repo.ObterPedidosPorAreaCozinhaAsync(areaCozinha))
                .ReturnsAsync(new List<PedidoProduto> { pedidoProduto1, pedidoProduto2, pedidoProduto3 });

            //UseCases
            var useCaseResponse = new ListarPedidosPorAreaResponse
            {
                Sucesso = true,
                Pedidos = new List<Pedido>
                {
                    new Pedido { Id = 1, PedidoProdutos = new List<PedidoProduto> { pedidoProduto1 } },
                    new Pedido { Id = 2, PedidoProdutos = new List<PedidoProduto> { pedidoProduto2 } }
                }
            };

            _pedidoProdutoRepositoryMock
                .Setup(x => x.ObterPedidosPorAreaCozinhaAsync(It.IsAny<AreaCozinha>()))
                .ReturnsAsync(new List<PedidoProduto> { pedidoProduto1, pedidoProduto2 });

            //ACT
            var result = await _useCase.HandleAsync(new ListarPedidosPorAreaRequest { AreaCozinha = areaCozinha });

            //ASSERT 
            Assert.True(result.Sucesso);
            Assert.Equal(2, result.Pedidos?.Count()); // Espera-se que apenas os pedidos 1 e 2 (da área Fritos) sejam retornados

            // Verifica se os pedidos retornados são os corretos (da área Fritos)
            foreach (var pedido in result.Pedidos)
            {
                var pedidoProduto = pedido.PedidoProdutos.First();
                Assert.Equal(areaCozinha, pedidoProduto.Produto.AreaCozinha);
            }
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarMensagemErroQuandoNaoEncontrarPedidos()
        {
            // Arrange
            var areaCozinha = AreaCozinha.Fritos;

            _pedidoProdutoRepositoryMock
                .Setup(repo => repo.ObterPedidosPorAreaCozinhaAsync(areaCozinha))
                .ReturnsAsync(new List<PedidoProduto>());

            // Act
            var result = await _useCase.HandleAsync(new ListarPedidosPorAreaRequest { AreaCozinha = areaCozinha });

            // Assert
            Assert.False(result.Sucesso);
            Assert.Equal("Nenhum pedido encontrado para a área de cozinha especificada.", result.Mensagem); //Mesma ideia, checo a string diretao, mas poderia ser algum tipo de .rsx da vida para garantir integridade.
        }
    }
}
