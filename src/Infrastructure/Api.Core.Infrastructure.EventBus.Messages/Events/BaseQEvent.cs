namespace Api.Core.Infrastructure.EventBus.Messages.Events
{
    public class BaseQEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public BaseQEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public BaseQEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
