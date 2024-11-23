using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(MacManagerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPedidosComProdutosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos) // Inclui os produtos associados
                    .ThenInclude(pp => pp.Produto) // Inclui o produto dentro de PedidoProduto
                .ToListAsync();
        }


    }
}
