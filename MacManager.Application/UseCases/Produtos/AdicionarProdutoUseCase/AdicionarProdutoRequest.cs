using MacManager.Domain.Entities;

namespace MacManager.Application.UseCases.Produtos.AdicionarProdutoUseCase
{
    public class AdicionarProdutoRequest
    {
        public required Produto Produto { get; set; } //Comecei a usar o required em tudo que obriga no .net, evita mta complicacao com nullables.
    }
}
