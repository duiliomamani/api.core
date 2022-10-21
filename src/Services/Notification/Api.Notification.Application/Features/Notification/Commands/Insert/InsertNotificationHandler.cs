using Api.Core.Infrastructure.EventBus.Messages.Events;
using AspNetCore.Common.Wrappers;
using AutoMapper;

namespace Api.Notification.Application.Features.Notification.Commands.Insert
{
    public class InsertNotificationHandler : IRequestHandlerWrapper<InsertNotificationCommand>
    {
        private readonly IMapper _mapper;
        //private readonly IMessageQueue _queue;
        private readonly MassTransit.IPublishEndpoint _queue;

        public InsertNotificationHandler(IMapper mapper, MassTransit.IPublishEndpoint queue)
        {
            _mapper = mapper;
            _queue = queue;
        }
        public async Task<Response> Handle(InsertNotificationCommand request, CancellationToken cancellationToken)
        {

            var eventMessage = _mapper.Map<NotificationQEvent>(request);
            await _queue.Publish(eventMessage);
            return new Response();
        }
    }
}
