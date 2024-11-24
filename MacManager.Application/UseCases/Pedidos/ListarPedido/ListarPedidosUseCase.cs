using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
namespace MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase
{
    public class ListarPedidosUseCase : IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;

        public ListarPedidosUseCase(
            IPedidoRepository pedidoRepository,
            IPedidoProdutoRepository pedidoProdutoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoProdutoRepository = pedidoProdutoRepository;
        }

        public async Task<ListarPedidosResponse> HandleAsync(ListarPedidosRequest request)
        {
            var pedidos = await _pedidoRepository.ObterTodosPedidosComProdutosAsync(request.Ativo);

            if (!pedidos.Any())
            {
                return new ListarPedidosResponse
                {
                    Sucesso = false,
                    Mensagem = "Nenhum pedido encontrado."
                };
            }

            // Preenchendo os PedidoProdutos para cada Pedido
            foreach (var pedido in pedidos)
            {
                var pedidoProdutos = await _pedidoProdutoRepository.ObterPedidoProdutosPorPedidoIdAsync(pedido.Id);
                pedido.PedidoProdutos = pedidoProdutos.ToList();
            }

            return new ListarPedidosResponse
            {
                Sucesso = true,
                Mensagem = "Pedidos listados com sucesso.",
                Pedidos = pedidos
            };
        }
    }
}
