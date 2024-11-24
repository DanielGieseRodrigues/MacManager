using MacManager.Application.Interfaces;
using MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.EditarProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ExcluirProdutoUseCase;
using MacManager.Application.UseCases.Produtos.ListarProdutosUseCase;
using Microsoft.AspNetCore.Mvc;

namespace MacManager.API.Controllers
{
    //Atributo de marcacao de rota da api.
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        //Injecoes de dependencia.
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

        //Daqui pra baixo é o crud basicao de Produto, ctrlc ctrl v quase, para gerenciamento dos produtos, não foi exigido no teste, mas pensei em fazer como um plus. Aqui cabia um validator da vida, ficaria bacana.
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

        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            var request = new ListarProdutosRequest();
            var response = await _listarProdutosUseCase.HandleAsync(request);

            //Truquezinho para representacao do enum e do ID que normalmente é escondido.
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

        //Aqui COM CERTEZA valeria ter uma regra para nao excluir produtos que estao envolvidos em transacoes de pedido. Mas por exemplificacao e simplificacao do exemplo , ignorei essa parte propositalmente, mas seria bacana pensar nisso como uma feature proxima.
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
