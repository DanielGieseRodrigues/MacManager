namespace MacManager.Domain.Entities
{
    //Representacao poco ( classe anemica) da entidade de PedidoProduto.
    public class PedidoProduto
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}