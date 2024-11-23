using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;

namespace MacManager.Application.UseCases.Produtos.EditarProdutoUseCase
{
    public class EditarProdutoUseCase : IUseCaseHandler<EditarProdutoRequest, EditarProdutoResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public EditarProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        //Metodo padrao de edicao, nao tem muita magia nele.
        public async Task<EditarProdutoResponse> HandleAsync(EditarProdutoRequest request)
        {
            if (request.Produto == null)
            {
                return new EditarProdutoResponse
                {
                    Sucesso = false,
                    Mensagem = "Produto não informado. Erro ao tentar editar o produto."
                };
            }

            var produtoExistente = await _produtoRepository.ObterPorIdAsync(request.Produto.Id);
            if (produtoExistente == null)
            {
                return new EditarProdutoResponse
                {
                    Sucesso = false,
                    Mensagem = "Produto não encontrado."
                };
            }

            // Atualiza as propriedades do produto
            produtoExistente.Nome = request.Produto.Nome;
            produtoExistente.Valor = request.Produto.Valor;
            produtoExistente.AreaCozinha = request.Produto.AreaCozinha;

            await _produtoRepository.AtualizarAsync(produtoExistente);

            return new EditarProdutoResponse
            {
                Sucesso = true,
                Mensagem = "Produto atualizado com sucesso.",
                Produto = produtoExistente
            };
        }
    }
}
