using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Repositories
{
    public class PedidoProdutoRepository : IPedidoProdutoRepository
    {
        private readonly MacManagerContext _context;

        public PedidoProdutoRepository(MacManagerContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterProdutosPorPedidoIdAsync(int pedidoId)
        {
            return await _context.PedidoProdutos
                .Where(pp => pp.PedidoId == pedidoId)
                .Select(pp => pp.Produto)
                .ToListAsync();
        }

        public async Task<IEnumerable<PedidoProduto>> ObterPedidoProdutosPorPedidoIdAsync(int pedidoId)
        {
            return await _context.PedidoProdutos
                .Include(pp => pp.Produto) // Inclui os produtos relacionados
                .Where(pp => pp.PedidoId == pedidoId)
                .ToListAsync();
        }
    }
}
