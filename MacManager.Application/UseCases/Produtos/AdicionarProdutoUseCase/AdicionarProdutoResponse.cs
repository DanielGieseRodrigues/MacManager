using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoResponse
    {
        public bool Sucesso { get; set; }
        public required string Mensagem { get; set; }
        public required Produto Produto { get; set; }
    }
}
