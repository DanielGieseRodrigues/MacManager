using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;

namespace MacManager.Application.Interfaces.Repositories
{
    //Representacao/Contrato dos repositorios, nao tem mto o que falar aqui de especial.
    public interface IPedidoProdutoRepository
    {
        Task<List<Produto>> ObterProdutosPorPedidoIdAsync(int pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterPedidoProdutosPorPedidoIdAsync(int pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterPedidosPorAreaCozinhaAsync(AreaCozinha areaCozinha);
    }
}
