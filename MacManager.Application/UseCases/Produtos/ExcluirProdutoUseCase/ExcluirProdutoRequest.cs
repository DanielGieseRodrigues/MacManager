namespace MacManager.Application.UseCases.Produtos.ExcluirProdutoUseCase
{
    public class ExcluirProdutoRequest
    {
        //Necessario somente o id, depois busco o objeto e deleto da memoria.
        public int Id { get; set; }
    }
}
