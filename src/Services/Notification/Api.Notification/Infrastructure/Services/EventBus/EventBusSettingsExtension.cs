using Api.Notification.Infrastructure.Services.EventBus.Consumers;
using Api.Notification.Infrastructure.Services.EventBus.Hubs;
using Api.Notification.Infrastructure.Services.EventBus.Hubs.Base;
using AspNetCore.Common.Statics;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Api.Notification.Infrastructure.Services.EventBus
{
    public static class EventBusSettingsExtension
    {
        public static IServiceCollection AddEventBusExtension(this IServiceCollection services)
        {
            // Inject to the site
            services.AddSignalR();

            services.AddSingleton<IHubConnections<string>, HubConnections<string>>();
            services.AddSingleton<NotificationHub>();

            var options = services.BuildServiceProvider().GetRequiredService<IOptions<List<AppSettings.ConnectionString>>>().Value;

            var mq = options.First(x => x.Core == AppSettings.CoreTypeEnum.RabbitMq);

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<NotificationConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mq.Server);
                    cfg.ReceiveEndpoint(mq.Connection, c =>
                    {
                        c.ConfigureConsumer<NotificationConsumer>(ctx);
                    });
                });
            });
            return services;
        }
    }
}
