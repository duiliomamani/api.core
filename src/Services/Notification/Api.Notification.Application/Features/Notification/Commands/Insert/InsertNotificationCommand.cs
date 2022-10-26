using Api.Core.Infrastructure.EventBus.Messages.Events;
using AspNetCore.Common.Mappings;
using AspNetCore.Common.Wrappers;
using AutoMapper;

namespace Api.Notification.Application.Features.Notification.Commands.Insert
{
    public class InsertNotificationCommand : IRequestWrapper, IMapFrom<NotificationQEvent>
    {
        public Guid ClientId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InsertNotificationCommand, NotificationQEvent>();
        }
    }
}
