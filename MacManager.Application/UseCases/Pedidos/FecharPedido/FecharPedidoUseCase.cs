using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.ValueObjects;

namespace MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase
{
    public class FecharPedidoUseCase : IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public FecharPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }


        //Metodo para encerrar o ciclo, sendo recusando ou concluindo o pedido.
        public async Task<FecharPedidoResponse> HandleAsync(FecharPedidoRequest request)
        {
            // Buscar o pedido pelo ID
            var pedido = await _pedidoRepository.ObterPorIdAsync(request.PedidoId);

            if (pedido == null)
            {
                return new FecharPedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "Pedido não encontrado."
                };
            }

            // Validar e atualizar o status, senao for status conhecido, cai fora por excessao.
            if (pedido.StatusPedido == StatusPedido.Fechado || pedido.StatusPedido == StatusPedido.Recusado)
            {
                return new FecharPedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "Não é possível atualizar um pedido que já está Fechado ou Recusado."
                };
            }

            pedido.StatusPedido = request.AtualizacaoStatus;
            pedido.DataConclusaoPedido = DateTime.Now;
            // Atualizar no repositório
            await _pedidoRepository.AtualizarAsync(pedido);

            return new FecharPedidoResponse
            {
                Sucesso = true,
                Mensagem = $"Pedido {request.AtualizacaoStatus} com sucesso.",
                Pedido = pedido
            };
        }
    }
}
