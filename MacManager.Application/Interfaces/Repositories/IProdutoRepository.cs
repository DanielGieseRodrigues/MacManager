using MacManager.Domain.Entities;

namespace MacManager.Application.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task AdicionarAsync(Produto produto);
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(Produto produto);
    }
}
