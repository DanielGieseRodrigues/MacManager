using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;

namespace MacManager.Application.UseCases.Produtos.ListarProdutosUseCase
{
    public class ListarProdutosUseCase : IUseCaseHandler<ListarProdutosRequest, ListarProdutosResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ListarProdutosUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        //listagem simples usando o repositorio , poderia ter mto mais tratativas num cenario real.
        public async Task<ListarProdutosResponse> HandleAsync(ListarProdutosRequest request)
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            return new ListarProdutosResponse
            {
                Sucesso = true,
                Mensagem = "Produtos listados com sucesso.",
                Produtos = produtos
            };
        }
    }
}
