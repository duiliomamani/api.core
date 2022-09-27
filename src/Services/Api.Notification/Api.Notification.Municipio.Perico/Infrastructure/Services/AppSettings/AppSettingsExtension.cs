using AspNetCore.Common.Statics;

namespace Api.Notification.Municipio.Perico.Infrastructure.Services
{
    public static class AppSettingsExtension
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // Inject to the site
            services.Configure<List<AppSettings.ConnectionString>>(configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettings.Auth>(configuration.GetSection("Auth"));
            services.Configure<AppSettings.Swagger>(configuration.GetSection("Swagger"));
            return services;
        }
    }
}
