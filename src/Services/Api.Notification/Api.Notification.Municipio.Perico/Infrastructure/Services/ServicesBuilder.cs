using AspNetCore.Common.Infrastructure.ApiVersioning;
using AspNetCore.Common.Infrastructure.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Notification.Municipio.Perico.Infrastructure.Services
{
    /// <summary>
    /// ServicesBuilder
    /// </summary>
    public static class ServicesBuilder
    {
        /// <summary>
        /// Services Configure all extension for example Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ServicesConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppSettings(configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            //RoutePrefix casesensitive LowerCaseURL
            services.Configure<RouteOptions>(config => config.LowercaseUrls = true);

            // Add services to the container.
            services.AddControllers().AddNewtonsoftJson(config =>
            {
                config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                config.SerializerSettings.Formatting = Formatting.Indented;
                config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                config.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
                config.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) };
            }).AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            //Cors Origins
            services.AddCors(config =>
            {
                config.AddDefaultPolicy(options =>
                    options.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //ApiVersion
            services.AddApiVersionExtension();
            //Swagger
            services.AddSwaggerExtension();

            return services;
        }
    }
}
