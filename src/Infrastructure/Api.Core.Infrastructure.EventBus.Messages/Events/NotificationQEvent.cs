namespace Api.Core.Infrastructure.EventBus.Messages.Events
{
    public class NotificationQEvent : BaseQEvent
    {
        public Guid ClientId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
