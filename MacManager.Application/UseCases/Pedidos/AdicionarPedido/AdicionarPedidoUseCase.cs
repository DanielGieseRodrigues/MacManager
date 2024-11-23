using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;
using MacManager.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase
{
    public class AdicionarPedidoUseCase : IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public AdicionarPedidoUseCase(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<AdicionarPedidoResponse> HandleAsync(AdicionarPedidoRequest request)
        {
            // Verifica se a lista de IDs de produtos não é nula ou vazia
            if (request.ListaIdsProdutos == null || !request.ListaIdsProdutos.Any())
            {
                return new AdicionarPedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "A lista de IDs de produtos está vazia ou nula."
                };
            }

            // Buscar produtos com base nos IDs
            var produtos = await Task.WhenAll(
                request.ListaIdsProdutos.Select(id => _produtoRepository.ObterPorIdAsync(id))
            );

            if (produtos.Any(p => p == null))
            {
                return new AdicionarPedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "Um ou mais IDs de produtos fornecidos não foram encontrados."
                };
            }

            // Criar o pedido
            var pedido = new Pedido
            {
                DataDoPedido = request.DataDoPedido,
                StatusPedido = StatusPedido.Aberto
            };

            // Adicionar os produtos ao pedido
            foreach (var produto in produtos.Where(p => p != null))
            {
                var pedidoProduto = pedido.PedidoProdutos
                    .FirstOrDefault(pp => pp.ProdutoId == produto.Id); // Verifica se o produto já está no pedido

                if (pedidoProduto != null)
                {
                    // Se o produto já existir no pedido, incrementa a quantidade
                    pedidoProduto.Quantidade++;
                }
                else
                {
                    // Se o produto não estiver no pedido, adiciona ele com quantidade 1
                    pedido.PedidoProdutos.Add(new PedidoProduto
                    {
                        Pedido = pedido,
                        ProdutoId = produto.Id,
                        Quantidade = 1
                    });
                }
            }

            // Salvar o pedido com os produtos associados
            await _pedidoRepository.AdicionarAsync(pedido);

            return new AdicionarPedidoResponse
            {
                Sucesso = true,
                Mensagem = "Pedido adicionado com sucesso.",
            };
        }
    }
}
