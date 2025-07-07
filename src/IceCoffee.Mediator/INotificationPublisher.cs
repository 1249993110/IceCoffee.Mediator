namespace IceCoffee.Mediator
{
    /// <summary>
    /// Represents an executor for handling notifications, encapsulating the handler instance and its callback method.
    /// </summary>
    /// <remarks>This record is used to associate a notification handler instance with its callback function,
    /// which processes notifications asynchronously. The callback function is expected to handle the notification and
    /// respect the provided cancellation token.</remarks>
    /// <param name="HandlerInstance">The instance of the notification handler. This object is responsible for processing notifications.</param>
    /// <param name="HandlerCallback">A delegate representing the callback method that processes the notification. The callback takes an <see
    /// cref="INotification"/> instance and a <see cref="CancellationToken"/> as parameters and returns a <see
    /// cref="Task"/> representing the asynchronous operation.</param>
    public record NotificationHandlerExecutor(object HandlerInstance, Func<INotification, CancellationToken, Task> HandlerCallback);

    /// <summary>
    /// Defines a publishing strategy for notifications.
    /// It determines how a collection of notification handlers are executed.
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a notification to the specified notification handler executors.
        /// </summary>
        /// <remarks>This method invokes each handler executor in the provided collection to process the
        /// given notification.  Handlers are executed asynchronously, and the method completes once all handlers have
        /// finished processing.</remarks>
        /// <param name="notificationExecutors">A collection of notification handler executors. Each executor in this collection will be invoked to handle the notification.</param>
        /// <param name="notification">The notification to be published. This represents the event or message to be processed by the handlers.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation. If cancellation is requested, the publishing process will
        /// be terminated.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task completes when all handlers have
        /// processed the notification.</returns>
        Task Publish(IEnumerable<NotificationHandlerExecutor> notificationExecutors, INotification notification, CancellationToken cancellationToken);
    }
}
