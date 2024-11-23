using MacManager.Domain.Entities;

namespace MacManager.Application.Interfaces.Repositories
{
    public interface IPedidoProdutoRepository
    {
        Task<List<Produto>> ObterProdutosPorPedidoIdAsync(int pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterPedidoProdutosPorPedidoIdAsync(int pedidoId);
    }
}
