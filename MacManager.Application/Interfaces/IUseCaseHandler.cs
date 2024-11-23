namespace MacManager.Application.Interfaces
{
    //Interface generica para useCase, também costumo sempre usar algo parecido com isso para modelos baseados no clean architcture.
    public interface IUseCaseHandler<in TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}
