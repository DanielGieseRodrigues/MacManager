using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Pedidos.ListarPedido
{
    public class ListarPedidosPorAreaResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
