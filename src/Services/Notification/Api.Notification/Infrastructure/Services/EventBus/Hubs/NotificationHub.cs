using Api.Notification.Infrastructure.Services.EventBus.Hubs.Base;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Api.Notification.Infrastructure.Services.EventBus.Hubs
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;
        private readonly IHubConnections<string> _connections;

        public NotificationHub(ILogger<NotificationHub> logger, IHubConnections<string> connections)
        {
            _logger = logger;
            _connections = connections;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (user != null)
                {
                    _connections.Add(user.Value, Context?.ConnectionId);
                }
                await base.OnConnectedAsync();
                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"NotificationHub Connected | {ex.Message} | {ex.StackTrace}");
                await Task.FromResult(false);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var user = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (user != null)
                {
                    _connections.Remove(user.Value, Context?.ConnectionId);
                }
                await base.OnDisconnectedAsync(exception);

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"NotificationHub Disconnected | {ex.Message} | {ex.StackTrace}");
                await Task.FromResult(false);
            }
        }
    }
}
