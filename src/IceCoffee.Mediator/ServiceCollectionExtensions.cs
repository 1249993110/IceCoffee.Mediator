using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IceCoffee.Mediator
{
    /// <summary>
    /// Provides IServiceCollection extension methods for service registration.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Scans the specified assemblies and automatically registers all concrete classes
        /// that implement the <see cref="INotificationHandler{TNotification}"/> interface.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <param name="lifetime">The service lifetime for the registered handlers (default is Scoped).</param>
        /// <param name="assemblies">The assemblies to scan. If none are provided, the calling assembly will be scanned.</param>
        /// <returns>The IServiceCollection to allow for method chaining.</returns>
        public static IServiceCollection AddMediatR<TNotificationPublisher>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient,
            params Assembly[] assemblies) where TNotificationPublisher : class, INotificationPublisher
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetCallingAssembly() };
            }

            // Find all concrete types (non-abstract, non-interface) that implement one or more INotificationHandler<T>
            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsAbstract == false && t.IsInterface == false && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)));

            foreach (var handlerType in handlerTypes)
            {
                // Get all INotificationHandler<T> interfaces that this type implements
                var serviceTypes = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

                // Register the handler for each interface it implements
                foreach (var serviceType in serviceTypes)
                {
                    services.Add(new ServiceDescriptor(serviceType, handlerType, lifetime));
                }
            }

            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton<INotificationPublisher, TNotificationPublisher>();

            return services;
        }

        /// <summary>
        /// Adds MediatR services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>This method registers MediatR services, including handlers, behaviors, and other
        /// related components,  using the specified service lifetime and scanning the provided assemblies for relevant
        /// types.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which MediatR services will be added.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> to use for MediatR handlers and related services. The default is <see
        /// langword="Transient"/>.</param>
        /// <param name="assemblies">The assemblies to scan for MediatR handlers, behaviors, and other related types.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddMediatR(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient,
            params Assembly[] assemblies)
        {
            return AddMediatR<ForeachAwaitPublisher>(services, lifetime, assemblies);
        }
    }
}
