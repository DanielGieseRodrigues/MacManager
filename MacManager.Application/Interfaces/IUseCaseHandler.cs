namespace MacManager.Application.Interfaces
{
    public interface IUseCaseHandler<in TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}
