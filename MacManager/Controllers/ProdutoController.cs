using MacManager.Application.Interfaces;
using MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.EditarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ExcluirProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ListarProdutosUseCase;
using Microsoft.AspNetCore.Mvc;

namespace MacManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IUseCaseHandler<AdicionarProdutoRequest, AdicionarProdutoResponse> _adicionarProdutoUseCase;
        private readonly IUseCaseHandler<EditarProdutoRequest, EditarProdutoResponse> _editarProdutoUseCase;
        private readonly IUseCaseHandler<ExcluirProdutoRequest, ExcluirProdutoResponse> _excluirProdutoUseCase;
        private readonly IUseCaseHandler<ListarProdutosRequest, ListarProdutosResponse> _listarProdutosUseCase;

        public ProdutoController(
            IUseCaseHandler<AdicionarProdutoRequest, AdicionarProdutoResponse> adicionarProdutoUseCase,
            IUseCaseHandler<EditarProdutoRequest, EditarProdutoResponse> editarProdutoUseCase,
            IUseCaseHandler<ExcluirProdutoRequest, ExcluirProdutoResponse> excluirProdutoUseCase,
            IUseCaseHandler<ListarProdutosRequest, ListarProdutosResponse> listarProdutosUseCase)
        {
            _adicionarProdutoUseCase = adicionarProdutoUseCase;
            _editarProdutoUseCase = editarProdutoUseCase;
            _excluirProdutoUseCase = excluirProdutoUseCase;
            _listarProdutosUseCase = listarProdutosUseCase;
        }

        // Método de Adicionar Produto (implementado)
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto([FromBody] AdicionarProdutoRequest request)
        {
            if (request == null)
                return BadRequest("Request mal formatado ou incorreto.");

            var response = await _adicionarProdutoUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(response);
        }

        // Método de Editar Produto (implementado)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProduto(int id, [FromBody] EditarProdutoRequest request)
        {
            if (request == null || request.Produto == null || id <= 0)
                return BadRequest("Produto não encontrado ou request mal formatado.");

            request.Produto.Id = id;
            var response = await _editarProdutoUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(response);
        }

        // Método de Listar Produtos (implementado)
        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            var request = new ListarProdutosRequest();
            var response = await _listarProdutosUseCase.HandleAsync(request);

            var result = response.Produtos.Select(p => new
            {
                p.Id,
                p.Nome,
                p.Valor,
                AreaCozinha = p.AreaCozinha.ToString()
            }).ToList();

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(result);
        }

        // Método de Deletar Produto (implementado)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var request = new ExcluirProdutoRequest { Id = id };
            var response = await _excluirProdutoUseCase.HandleAsync(request);

            if (!response.Sucesso)
                return BadRequest(response.Mensagem);

            return Ok(response);
        }
    }
}
