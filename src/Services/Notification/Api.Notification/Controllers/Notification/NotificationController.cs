using Api.Notification.Application.Features.Notification.Commands.Insert;
using Api.Notification.Controllers.Base;
using AspNetCore.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Notification.Controllers.Notification
{
    [ApiVersion("2.0")]
    public class NotificationController : ApiController
    {
        public NotificationController(IConfiguration configuration, 
            IWebHostEnvironment hostingEnvironment) 
            : base(configuration, hostingEnvironment)
        {
        }

        [HttpPost("insert")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<IActionResult> Insert([FromBody] InsertNotificationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
