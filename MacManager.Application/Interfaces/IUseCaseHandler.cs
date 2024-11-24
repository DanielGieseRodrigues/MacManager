namespace MacManager.Application.Interfaces
{
    //Interface generica para useCase, costumo sempre usar algo parecido com isso para modelos baseados no clean architeture, para abstrair a logicaa de processamento das use cases.
    public interface IUseCaseHandler<in TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}
