using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Infra.Data;

namespace MacManager.Infra.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MacManagerContext context) : base(context)
        {
        }
        // Métodos específicos para Produto, caso necessário
    }
}
