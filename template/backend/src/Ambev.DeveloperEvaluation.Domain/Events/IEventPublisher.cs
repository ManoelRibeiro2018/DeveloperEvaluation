namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken);
    }
}
