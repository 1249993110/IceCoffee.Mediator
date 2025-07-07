namespace IceCoffee.Mediator
{
    /// <summary>
    /// Provides a mechanism to asynchronously publish notifications to a collection of notification handler executors.
    /// </summary>
    /// <remarks>This publisher iterates over the provided collection of <see
    /// cref="NotificationHandlerExecutor"/> instances and invokes their handler callbacks asynchronously, passing the
    /// notification and cancellation token.</remarks>
    public sealed class ForeachAwaitPublisher : INotificationPublisher
    {
        /// <inheritdoc/>
        public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var item in handlerExecutors)
            {
                await item.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
