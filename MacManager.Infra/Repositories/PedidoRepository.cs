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

        // Essa classe é um exemplo de uma classe com metodo de acesso a dados especifico, que ai sim, nao existe no baseRepository.
        public async Task<IEnumerable<Pedido>> ObterTodosPedidosComProdutosAsync()
        {
            //Dando o include para forçar o tracking do entity, importancia do ThenInclude tbm destacada.

            return await _context.Pedidos
                .Include(p => p.PedidoProdutos) 
                    .ThenInclude(pp => pp.Produto) 
                .ToListAsync();
        }


    }
}
