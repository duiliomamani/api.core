namespace Api.Core.Infrastructure.EventBus.Messages.Events
{
    public abstract class BaseQEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; }
    }
}
