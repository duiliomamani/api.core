using AspNetCore.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Notification.Municipio.Perico.Controllers.Base
{
    //[AuthorizeApi]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
    public abstract class ApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// IMediator sender
        /// </summary>
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();


        /// <summary>
        /// Constructor ApiController
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public ApiController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public string SiteUrl => string.Format("{0}://{1}", Request.Scheme, Request.Host);
    }
}
