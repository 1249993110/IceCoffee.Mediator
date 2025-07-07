# IceCoffee.Mediator

| Package | NuGet Stable | Downloads |
| ------- | ------------ | --------- |
| [IceCoffee.Mediator](https://www.nuget.org/packages/IceCoffee.Mediator/) | [![IceCoffee.Mediator](https://img.shields.io/nuget/v/IceCoffee.Mediator.svg)](https://www.nuget.org/packages/IceCoffee.Mediator/) | [![IceCoffee.Mediator](https://img.shields.io/nuget/dt/IceCoffee.Mediator.svg)](https://www.nuget.org/packages/IceCoffee.Mediator/) |

## Description

A lightweight, type-safe Mediator for .NET designed for high performance and easy integration. It helps you decouple components in your application by sending messages and handling them through a central mediator.

## Key Features

* **Inspired by MediatR and mitt.js**: Combines robustness with a simple core concept.
* **Seamless DI Integration**: Designed to work perfectly with `Microsoft.Extensions.DependencyInjection`.
* **Automatic Handler Registration**: Scans assemblies to find and register notification handlers automatically.
* **Pluggable Publishing Strategies**: Customize how notifications are handled, enabling advanced scenarios like parallel execution or robust error handling.
* **Zero External Dependencies**: The core library has no external dependencies.

## Quick Start

1.  **Define a Notification:**
    ```csharp
    public class UserCreatedNotification : INotification
    {
        public int UserId { get; set; }
    }
    ```

2.  **Create a Handler:**
    ```csharp
    public class WelcomeEmailSender : INotificationHandler<UserCreatedNotification>
    {
        public void Handle(UserCreatedNotification notification)
        {
            Console.WriteLine($"Welcome email sent to user {notification.UserId}!");
        }
    }
    ```

3.  **Configure DI and Publish:**
    ```csharp
    var services = new ServiceCollection();

    // Register Mediator and automatically find and register all handlers in the current assembly
    services.AddMediator();
    // Or use a specific publisher strategy and lifetime and assemblies
    services.AddMediator<ForeachAwaitPublisher>(ServiceLifetime.Singleton, typeof(Program).Assembly);

    var serviceProvider = services.BuildServiceProvider();
    var mediator = serviceProvider.GetRequiredService<IMediator>();

    // Publish the notification
    mediator.Publish(new UserCreatedNotification { UserId = 101 });
    ```