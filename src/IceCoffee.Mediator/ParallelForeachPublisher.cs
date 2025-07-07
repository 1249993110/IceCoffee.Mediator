namespace IceCoffee.Mediator
{
    /// <summary>
    /// Executes notification handlers in parallel using asynchronous processing.
    /// </summary>
    /// <remarks>This publisher utilizes <see cref="Parallel.ForEachAsync{TSource}(IEnumerable{TSource}, CancellationToken, Func{TSource, CancellationToken, ValueTask})"/> to invoke the provided
    /// notification handlers concurrently. It is designed to efficiently handle scenarios where multiple handlers need
    /// to process the same notification.</remarks>
    public sealed class ParallelForeachPublisher : INotificationPublisher
    {
        /// <inheritdoc/>
        public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            return Parallel.ForEachAsync(handlerExecutors, cancellationToken, async (handler, token) =>
            {
                await handler.HandlerCallback(notification, token).ConfigureAwait(false);
            });
        }
    }
}
