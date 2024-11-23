using MacManager.Domain.Entities;

namespace MacManager.Application.Interfaces.Repositories
{
    //Representacao/Contrato dos repositorios, nao tem mto o que falar aqui de especial.
    public interface IPedidoRepository
    {
        Task AdicionarAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task<Pedido?> ObterPorIdAsync(int id);
        Task AtualizarAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ObterTodosPedidosComProdutosAsync();
    }
}
