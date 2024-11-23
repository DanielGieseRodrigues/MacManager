using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase
{
    public class FecharPedidoResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public Pedido? Pedido { get; set; }
    }
}
