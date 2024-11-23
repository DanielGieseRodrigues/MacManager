using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos.ListarPedido;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;

namespace MacManager.Application.UseCases.Pedidos
{
    public class ListarPedidosPorAreaUseCase : IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;

        public ListarPedidosPorAreaUseCase(IPedidoRepository pedidoRepository, IPedidoProdutoRepository pedidoProdutoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoProdutoRepository = pedidoProdutoRepository;
        }

        public async Task<ListarPedidosPorAreaResponse> HandleAsync(ListarPedidosPorAreaRequest request)
        {
            // Buscar os PedidoProdutos filtrados pela área de cozinha
            var pedidoProdutos = await _pedidoProdutoRepository.ObterPedidosPorAreaCozinhaAsync(request.AreaCozinha);

            if (!pedidoProdutos.Any())
            {
                return new ListarPedidosPorAreaResponse
                {
                    Sucesso = false,
                    Mensagem = "Nenhum pedido encontrado para a área de cozinha especificada."
                };
            }
            // Agrupando os PedidoProdutos por PedidoId
            var pedidos = pedidoProdutos
                .GroupBy(pp => pp.PedidoId)
                .Select(group => new Pedido
                {
                    Id = group.Key,
                    PedidoProdutos = group.ToList() // Agrupar os PedidoProdutos para esse pedido
                })
                .ToList();

            return new ListarPedidosPorAreaResponse
            {
                Sucesso = true,
                Mensagem = "Pedidos filtrados por área de cozinha listados com sucesso.",
                Pedidos = pedidos
            };
        }
    }
}
