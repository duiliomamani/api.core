using Api.Core.Infrastructure.EventBus.Messages.Events;
using Api.Notification.Infrastructure.Services.EventBus.Hubs;
using Api.Notification.Infrastructure.Services.EventBus.Hubs.Base;
using AspNetCore.Common.Utils;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.Notification.Infrastructure.Services.EventBus.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationQEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IHubConnections<string> _connections;

        public NotificationConsumer(IMediator mediator, IMapper mapper, ILogger<NotificationConsumer> logger,
            IHubContext<NotificationHub> hub, IHubConnections<string> connections)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public async Task Consume(ConsumeContext<NotificationQEvent> context)
        {
            var clientConnections = _connections.GetConnections(context.Message.UserId);

            foreach (var cli in clientConnections)
            {
                 await _hub.Clients.Client(cli).SendAsync("GetNotification", JsonHelper.Serialize(context.Message), "test", "asdakjasldfkadsfladskjf");
                _logger.LogInformation($"Message {JsonHelper.Serialize(context.Message)}");
            }
        }
    }
}
