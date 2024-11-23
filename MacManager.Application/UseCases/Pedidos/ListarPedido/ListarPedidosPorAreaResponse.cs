using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Pedidos.ListarPedido
{
    public class ListarPedidosPorAreaResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public IEnumerable<Pedido>? Pedidos { get; set; }
    }
}
