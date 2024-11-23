using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase;
using MacManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MacManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse> _adicionarPedidoUseCase;
        private readonly IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse> _fecharPedidoUseCase;
        private readonly IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse> _listarPedidosUseCase;
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository; // Repositório para acessar a tabela de junção

        public PedidoController(
            IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse> adicionarPedidoUseCase,
            IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse> fecharPedidoUseCase,
            IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse> listarPedidosUseCase,
            IPedidoProdutoRepository pedidoProdutoRepository) // Injeção do repositório de PedidoProduto
        {
            _adicionarPedidoUseCase = adicionarPedidoUseCase;
            _fecharPedidoUseCase = fecharPedidoUseCase;
            _listarPedidosUseCase = listarPedidosUseCase;
            _pedidoProdutoRepository = pedidoProdutoRepository; // Atribuição
        }

        // Método para adicionar um novo pedido
        [HttpPost]
        public async Task<IActionResult> AdicionarPedido([FromBody] AdicionarPedidoRequest request)
        {
            if (request == null)
                return BadRequest("Request mal formatado ou incorreto.");

            var response = await _adicionarPedidoUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(response);
        }

        // Método para fechar um pedido (marcar como concluído)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> FecharPedido(int id, [FromBody] FecharPedidoRequest request)
        {
            if (request == null)
                return BadRequest("Request mal formatado ou nulo.");

            request.PedidoId = id;

            var response = await _fecharPedidoUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var request = new ListarPedidosRequest();
            var response = await _listarPedidosUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return NotFound(response.Mensagem);

            // Formatação para retornar os pedidos com dados refinados
            var result = await Task.WhenAll(response.Pedidos.Select(async p =>
            {
                // Buscar os produtos associados ao pedido pela tabela de junção usando o repositório
                var pedidoProdutos = await _pedidoProdutoRepository.ObterPedidoProdutosPorPedidoIdAsync(p.Id);

                return new
                {
                    p.Id,
                    Produtos = pedidoProdutos.Select(pp => new
                    {
                        pp.Produto.Id,
                        pp.Produto.Nome,
                        pp.Produto.Valor,
                        AreaCozinha = pp.Produto.AreaCozinha.ToString(), // Enum como string
                        Quantidade = pp.Quantidade // Aqui adiciona a quantidade do produto
                    }).ToList(),
                    Status = p.StatusPedido.ToString(), // Enum como string
                    DataDoPedido = p.DataDoPedido.ToString("yyyy-MM-dd HH:mm:ss"), // Formato de data
                    DataConclusao = p.DataConclusaoPedido?.ToString("yyyy-MM-dd HH:mm:ss") // Formato de data
                };
            }).ToList());

            return Ok(result);
        }

    }
}
