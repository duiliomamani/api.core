using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;

        public LoggingBehaviour(ILogger<TRequest> logger, IHttpContextAccessor http)
        {
            _logger = logger;
            _http = http;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var user = await GetUserID();

            var ipAdress = _http.HttpContext.Connection.RemoteIpAddress.ToString();

            _logger.LogInformation("CQRS + MedaitR Request: {Name} {UserId} {IpAdress} {@Request}",
                requestName, user, ipAdress, request);
        }

        public async Task<string> GetUserID()
        {
            /** Here we need inject the identity server or application identity to get the user ID*/
            return await Task.FromResult("User ID");
        }
    }
}
