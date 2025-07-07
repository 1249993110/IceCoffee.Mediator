namespace IceCoffee.Mediator
{
    /// <summary>
    /// Publish a notification or event through the mediator pipeline to be handled by multiple handlers.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Asynchronously send a notification to multiple handlers
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification">Notification object</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A task that represents the publish operation.</returns>
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
    }
}
