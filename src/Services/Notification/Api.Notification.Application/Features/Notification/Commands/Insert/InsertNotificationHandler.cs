using Api.Core.Infrastructure.EventBus.Messages.Events;
using AspNetCore.Common.Wrappers;
using AutoMapper;
using MassTransit;

namespace Api.Notification.Application.Features.Notification.Commands.Insert
{
    public class InsertNotificationHandler : IRequestHandlerWrapper<InsertNotificationCommand>
    {
        private readonly IMapper _mapper;
        //private readonly IMessageQueue _queue;
        private readonly IPublishEndpoint _queue;

        public InsertNotificationHandler(IMapper mapper, IPublishEndpoint queue)
        {
            _mapper = mapper;
            _queue = queue;
        }
        public async Task<TResponse> Handle(InsertNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var notification = _mapper.Map<NotificationQEvent>(request);
                await _queue.Publish(notification);
            }

            catch { };

            return new TResponse();
        }
    }
}
