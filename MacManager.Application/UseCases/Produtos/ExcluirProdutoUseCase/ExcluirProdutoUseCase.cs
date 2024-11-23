using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;

namespace MacManager.Application.UseCases.Produtos.ExcluirProdutoUseCase
{
    public class ExcluirProdutoUseCase : IUseCaseHandler<ExcluirProdutoRequest, ExcluirProdutoResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ExcluirProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ExcluirProdutoResponse> HandleAsync(ExcluirProdutoRequest request)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(request.Id);
            if (produto == null)
            {
                return new ExcluirProdutoResponse
                {
                    Sucesso = false,
                    Mensagem = "Produto não encontrado."
                };
            }

            await _produtoRepository.RemoverAsync(produto);

            return new ExcluirProdutoResponse
            {
                Sucesso = true,
                Mensagem = "Produto excluído com sucesso."
            };
        }
    }
}
