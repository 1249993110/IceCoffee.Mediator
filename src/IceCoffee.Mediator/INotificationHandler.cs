namespace IceCoffee.Mediator
{
    /// <summary>
    /// Defines a handler for a specific notification type.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification to be handled.</typeparam>
    public interface INotificationHandler<in TNotification> where TNotification : INotification
    {
        /// <summary>
        /// Handles the given notification.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
