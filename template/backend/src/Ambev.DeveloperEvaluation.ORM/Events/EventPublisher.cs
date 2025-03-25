using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(ILogger<EventPublisher> logger)
        {
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event published successfully");
            await Task.CompletedTask;
        }
    }
}
