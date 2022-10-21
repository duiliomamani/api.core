using Api.Notification.Infrastructure.App;
using Api.Notification.Infrastructure.Services;

namespace Api.Notification
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Enviroment
        /// </summary>
        private IWebHostEnvironment Enviroment { get; }

        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration config, IWebHostEnvironment environment)
        {
            Configuration = config;
            Enviroment = environment;
        }

        /// <summary>
        /// Configure Services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ServicesConfigure(Configuration);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.AppConfigure(Enviroment);
        }
    }

}
