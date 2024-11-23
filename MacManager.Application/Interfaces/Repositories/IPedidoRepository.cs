using MacManager.Domain.Entities;

namespace MacManager.Application.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task AdicionarAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task<Pedido?> ObterPorIdAsync(int id);
        Task AtualizarAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ObterTodosPedidosComProdutosAsync();
    }
}
