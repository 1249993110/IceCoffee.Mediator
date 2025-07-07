using Microsoft.Extensions.DependencyInjection;

namespace IceCoffee.Mediator
{
    internal sealed class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationPublisher _publisher;


        public Mediator(IServiceProvider serviceProvider, INotificationPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
        }


        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            // 1. Resolve all relevant handlers from the DI container.
            var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

            // 2. Wrap each handler's Handle call into a NotificationHandlerExecutor.
            var handlerExecutors = handlers
                .Select(handler => new NotificationHandlerExecutor(handler, (notification, cancellationToken) => handler.Handle((TNotification)notification, cancellationToken)));

            // 3. Delegate the actual execution to the configured publisher strategy.
            return _publisher.Publish(handlerExecutors, notification, cancellationToken);
        }
    }
}
