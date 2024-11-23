using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using MacManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Repositories
{
    //Classe exemplo de repositorio com alguns metodos mais especificos, boa parte deles para filtrar ou aumentar o tracking do ef com o include.
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
                .Include(pp => pp.Produto) 
                .Where(pp => pp.PedidoId == pedidoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PedidoProduto>> ObterPedidosPorAreaCozinhaAsync(AreaCozinha areaCozinha)
        {
            // Buscar pedidos com produtos filtrados por área de cozinha (usando o enum) e retornar PedidoProduto, Uso do include para tracking
            return await _context.PedidoProdutos
                .Include(pp => pp.Pedido) 
                .Include(pp => pp.Produto) 
                .Where(pp => pp.Produto.AreaCozinha == areaCozinha) 
                .ToListAsync();
        }
    }
}
