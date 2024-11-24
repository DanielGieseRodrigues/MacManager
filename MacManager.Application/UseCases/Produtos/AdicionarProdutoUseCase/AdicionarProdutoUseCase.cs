using MacManager.Application.Interfaces;
using MacManager.Application.Interfaces.Repositories;
using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoUseCase : IUseCaseHandler<AdicionarProdutoRequest, AdicionarProdutoResponse>
    {
        private readonly IProdutoRepository _produtoRepository;

        public AdicionarProdutoUseCase(IProdutoRepository pedidoRepository)
        {
            _produtoRepository = pedidoRepository;
        }

        //Aqui poderia ter validators da vida, ou até mesmo ter inventado mais regras de negócio, mas acho que pro exemplo já basta.
        public async Task<AdicionarProdutoResponse> HandleAsync(AdicionarProdutoRequest request)
        {
            if (request.Produto == null)
            {
                return new AdicionarProdutoResponse
                {
                    Sucesso = false,
                    Produto = new Produto { Nome = "Produto não informado." },
                    Mensagem = "Produto não informado. Erro ao inserir produto na base de dados."
                };
            }
            if (request.Produto.Valor < 0)
            {
                return new AdicionarProdutoResponse
                {
                    Sucesso = false,
                    Produto = new Produto { Nome = "Valor abaixo de zero." },
                    Mensagem = "Valor do produto não pode ser inferior a zero !"
                };
            }

            //Chama o repositorio para adicionar o item no _context.
            await _produtoRepository.AdicionarAsync(request.Produto);

            return new AdicionarProdutoResponse
            {
                Sucesso = true,
                Mensagem = "Produto adicionado com sucesso.",
                Produto = request.Produto
            };
        }
    }
}
