using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
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

        public async Task<IEnumerable<Pedido>> ObterPedidosPorAreaCozinhaAsync(AreaCozinha areaCozinha)
        {
            // Buscar pedidos com produtos filtrados por área de cozinha (usando o enum)
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos) // Carregar os produtos relacionados
                 .ThenInclude(pp => pp.Produto) // Inclui o produto dentro de PedidoProduto
                .Where(p => p.PedidoProdutos.Any(prod => prod.Produto.AreaCozinha == areaCozinha)) // Filtrar por área de cozinha (usando o enum)
                .ToListAsync();
        }
    }
}
