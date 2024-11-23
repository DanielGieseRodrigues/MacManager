using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoResponse
    {
        public bool Sucesso { get; set; }
        public required string Mensagem { get; set; } //Comecei a usar o required em tudo que obriga no .net, evita mta complicacao com nullables.
        public required Produto Produto { get; set; }
    }
}
