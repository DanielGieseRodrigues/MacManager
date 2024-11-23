using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.ListarProdutosUseCase
{
    public class ListarProdutosResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
