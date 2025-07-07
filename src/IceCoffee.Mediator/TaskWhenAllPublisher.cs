namespace IceCoffee.Mediator
{
    /// <summary>
    /// Publishes a notification to all specified notification handler executors asynchronously.
    /// </summary>
    /// <remarks>This implementation invokes the <see cref="Task.WhenAll(IEnumerable{Task})"/>
    /// method for each executor in the provided collection and waits for all tasks to complete. If any handler throws an exception, the returned task will reflect the aggregate exception.</remarks>
    public sealed class TaskWhenAllPublisher : INotificationPublisher
    {
        /// <inheritdoc/>
        public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            return Task.WhenAll(handlerExecutors.Select(item => item.HandlerCallback(notification, cancellationToken)));
        }
    }
}
