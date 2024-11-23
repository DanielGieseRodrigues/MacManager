using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoRequest
    {
        public required Produto Produto { get; set; }
    }
}
