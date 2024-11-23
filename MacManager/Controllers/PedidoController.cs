using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Application.UseCases.Pedidos.AdicionarPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.FecharPedidoUseCase;
using MacManager.Application.UseCases.Pedidos.ListarPedido;
using MacManager.Application.UseCases.Pedidos.ListarPedidosUseCase;
using MacManager.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace MacManager.API.Controllers
{
    //Atributo de marcacao de rota da api.
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        //DI
        private readonly IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse> _adicionarPedidoUseCase;
        private readonly IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse> _fecharPedidoUseCase;
        private readonly IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse> _listarPedidosUseCase;
        private readonly IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse> _listarPedidoPorAreaUseCase;

        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;

        public PedidoController(
            IUseCaseHandler<AdicionarPedidoRequest, AdicionarPedidoResponse> adicionarPedidoUseCase,
            IUseCaseHandler<FecharPedidoRequest, FecharPedidoResponse> fecharPedidoUseCase,
            IUseCaseHandler<ListarPedidosRequest, ListarPedidosResponse> listarPedidosUseCase,
            IUseCaseHandler<ListarPedidosPorAreaRequest, ListarPedidosPorAreaResponse> listarperdidoporareausecase,
            IPedidoProdutoRepository pedidoProdutoRepository)
        {
            _adicionarPedidoUseCase = adicionarPedidoUseCase;
            _fecharPedidoUseCase = fecharPedidoUseCase;
            _listarPedidosUseCase = listarPedidosUseCase;
            _pedidoProdutoRepository = pedidoProdutoRepository; // Atribuição
            _listarPedidoPorAreaUseCase = listarperdidoporareausecase;
        }

       //Metodo que INICIA o Pedido.
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

        // Método para ENCERRAR um pedido, seja RECUSANDO ou Concluindo ( Fechando ).
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

        //Metodo para listar todos os pedidos, Daria pra ter varias variacoes, pedindos em aberto, pedindos fechados, etc etc. Ou ter mais parametros tbm, mas pro exemplo assim serve até demais.
        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var request = new ListarPedidosRequest();
            var response = await _listarPedidosUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return NotFound(response.Mensagem);

            // Formatação para retornar os pedidos formatadinhos. Task WhenAll para garantir a sincronicidade.
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
                        AreaCozinha = pp.Produto.AreaCozinha.ToString(), 
                        Quantidade = pp.Quantidade 
                    }).ToList(),
                    Status = p.StatusPedido.ToString(), 
                    DataDoPedido = p.DataDoPedido.ToString("yyyy-MM-dd HH:mm:ss"), // formatacao basica de data
                    DataConclusao = p.DataConclusaoPedido?.ToString("yyyy-MM-dd HH:mm:ss") /* formatacao basica de data */
                };
            }).ToList());

            return Ok(result);
        }


        // Metodo para listar os pedidos de cada Area da cozinha, especificadamente. 
        [HttpGet("area-cozinha")]
        public async Task<IActionResult> ListarPedidosPorArea([FromQuery] AreaCozinha areaCozinha)
        {
            var request = new ListarPedidosPorAreaRequest { AreaCozinha = areaCozinha };
            var response = await _listarPedidoPorAreaUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return NotFound(response.Mensagem);

            var result = response.Pedidos.Select(p => new
            {
                p.Id,
                Produtos = p.PedidoProdutos.Select(pp => new
                {
                    pp.Produto.Id,
                    pp.Produto.Nome,
                    pp.Produto.Valor,
                    AreaCozinha = pp.Produto.AreaCozinha.ToString(),
                    pp.Quantidade
                }).ToList()
            }).ToList();

            return Ok(result);
        }

    }
}
