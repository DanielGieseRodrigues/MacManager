using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.EditarProdutoUseCase
{
    public class EditarProdutoResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public Produto Produto { get; set; }
    }
}
