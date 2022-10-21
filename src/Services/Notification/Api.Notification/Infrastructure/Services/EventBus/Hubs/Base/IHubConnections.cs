namespace Api.Notification.Infrastructure.Services.EventBus.Hubs.Base
{
    public interface IHubConnections<T>
    {
        void Add(T key, string connectionId);
        IEnumerable<string> GetConnections(T key);
        void Remove(T key, string connectionId);
        Dictionary<T, HashSet<string>> Connections();
    }
}
