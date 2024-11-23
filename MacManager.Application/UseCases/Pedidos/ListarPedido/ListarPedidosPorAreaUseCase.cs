using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;

namespace MacManager.Application.UseCases.Pedidos.ListarPedido
{
    public class ListarPedidosPorAreaUseCase : IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ListarPedidosPorAreaUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ListarPedidosPorAreaResponse> HandleAsync(ListarPedidosPorAreaRequest request)
        {
            // Verificar se a área de cozinha foi fornecida
            if (request.AreaCozinha == null)
            {
                return new ListarPedidosPorAreaResponse
                {
                    Sucesso = false,
                    Mensagem = "Área de cozinha não fornecida."
                };
            }

            // Buscar pedidos com produtos filtrados por área de cozinha
            var pedidos = await _pedidoRepository.ObterPedidosPorAreaCozinhaAsync(request.AreaCozinha);

            if (!pedidos.Any())
            {
                return new ListarPedidosPorAreaResponse
                {
                    Sucesso = false,
                    Mensagem = "Nenhum pedido encontrado para a área de cozinha fornecida."
                };
            }

            return new ListarPedidosPorAreaResponse
            {
                Sucesso = true,
                Mensagem = "Pedidos listados com sucesso.",
                Pedidos = pedidos.ToList()
            };
        }
    }
}
