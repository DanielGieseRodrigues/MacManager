using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase
{
    public class ListarPedidosResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public IEnumerable<Pedido>? Pedidos { get; set; }
    }
}
