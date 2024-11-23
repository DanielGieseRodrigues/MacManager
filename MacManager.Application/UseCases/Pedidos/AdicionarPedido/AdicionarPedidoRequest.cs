namespace MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase
{
    public class AdicionarPedidoRequest
    {
        public List<int> ListaIdsProdutos { get; set; } = [];
        public DateTime DataDoPedido { get; set; }
    }
}
