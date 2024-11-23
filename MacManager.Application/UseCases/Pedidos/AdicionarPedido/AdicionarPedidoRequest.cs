namespace MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase
{
    public class AdicionarPedidoRequest
    {
        //Lista dos identificadores de produtos escolhidos pelo usuario.
        public List<int> ListaIdsProdutos { get; set; } = [];
        public DateTime DataDoPedido { get; set; }
    }
}
