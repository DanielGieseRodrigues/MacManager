using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;

namespace MacManager.Application.Interfaces.Repositories
{
    public interface IPedidoProdutoRepository
    {
        Task<List<Produto>> ObterProdutosPorPedidoIdAsync(int pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterPedidoProdutosPorPedidoIdAsync(int pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterPedidosPorAreaCozinhaAsync(AreaCozinha areaCozinha);
    }
}
