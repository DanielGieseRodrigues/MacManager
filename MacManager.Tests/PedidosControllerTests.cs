using MacManager.API.Controllers;
using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.ListarPedido;
using MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MacManager.Tests
{

    //Caberiam muito mais testes em um cenario real, incluindo sonar cube, considero uma meta de 70% de cobertura uma boa margem, aqui deixei a cobertura baixa e foquei mais na parte didatica / exemplo.
    public class PedidosControllerTests
    {
        private readonly Mock<IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse>> _adicionarPedidoUseCaseMock;
        private readonly Mock<IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse>> _fecharPedidoUseCaseMock;
        private readonly Mock<IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse>> _listarPedidosUseCaseMock;
        private readonly Mock<IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse>> _listarPedidosPorAreaUseCaseMock;
        private readonly Mock<IPedidoProdutoRepository> _pedidoProdutoRepositoryMock;
        private readonly PedidoController _controller;

        public PedidosControllerTests()
        {
            //Criacao de mocks para casos de uso
            _adicionarPedidoUseCaseMock = new Mock<IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse>>();
            _fecharPedidoUseCaseMock = new Mock<IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse>>();
            _listarPedidosUseCaseMock = new Mock<IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse>>();
            _listarPedidosPorAreaUseCaseMock = new Mock<IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse>>();
            _pedidoProdutoRepositoryMock = new Mock<IPedidoProdutoRepository>();

            //Fazendo a DI na controller
            _controller = new PedidoController(
                _adicionarPedidoUseCaseMock.Object,
                _fecharPedidoUseCaseMock.Object,
                _listarPedidosUseCaseMock.Object,
                _listarPedidosPorAreaUseCaseMock.Object,
                _pedidoProdutoRepositoryMock.Object
            );
        }

        [Fact]
        public async Task ListarPedidosPorArea_DeveRetornarOkQuandoPedidosExistirem()
        {
            //Arrange
            var areaCozinha = AreaCozinha.Fritos;
            var response = new ListarPedidosPorAreaResponse
            {
                Sucesso = true,
                Pedidos = new List<Pedido> { new Pedido { Id = 1 } }
            };

            _listarPedidosPorAreaUseCaseMock
                .Setup(x => x.HandleAsync(It.IsAny<ListarPedidosPorAreaRequest>()))
                .ReturnsAsync(response);

            //Act
            var result = await _controller.ListarPedidosPorArea(areaCozinha);

            //ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<IEnumerable<dynamic>>(okResult.Value);
            Assert.Single(resultValue);  // Checadinha para ver se voltou algum registro.
        }

        [Fact]
        public async Task ListarPedidosPorArea_DeveRetornarNotFoundQuandoNaoExistiremPedidos()
        {
            // Arrange
            var areaCozinha = AreaCozinha.Saladas;
            var response = new ListarPedidosPorAreaResponse
            {
                Sucesso = false,
                Mensagem = "Nenhum pedido encontrado para a área de cozinha especificada."
            };

            _listarPedidosPorAreaUseCaseMock
                .Setup(x => x.HandleAsync(It.IsAny<ListarPedidosPorAreaRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.ListarPedidosPorArea(areaCozinha);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = Assert.IsType<string>(notFoundResult.Value);
            Assert.Equal("Nenhum pedido encontrado para a área de cozinha especificada.", resultValue); //Checa mensagem padrao do sistema, para maior controle ainda seria legal usar uns .rsx ( resources ) para garantir a string.
        }
    }
}