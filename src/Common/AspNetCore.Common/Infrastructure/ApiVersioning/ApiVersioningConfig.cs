using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Common.Infrastructure.ApiVersioning
{
    public static class ApiVersioningConfig
    {
        /// <summary>
        /// Add ApiVersionExtension
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersionExtension(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VV";
                config.SubstituteApiVersionInUrl = true;
            }).AddApiVersioning(config =>
                {
                    // ReportApiVersions will return the "api-supported-versions" and "api-deprecated-versions" headers.
                    config.ReportApiVersions = true;
                    // Set a default version when it's not provided,
                    // e.g., for backward compatibility when applying versioning on existing APIs
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    // Specify the default API Version as 1.0
                    config.DefaultApiVersion = ApiVersion.Default;
                    config.UseApiBehavior = false;
                    // Combine (or not) API Versioning Mechanisms:
                    //config.ApiVersionReader = ApiVersionReader.Combine(
                    //        // The Default versioning mechanism which reads the API version from the "api-version" Query String paramater.
                    //        new QueryStringApiVersionReader("api-version"),
                    //        // Use the following, if you would like to specify the version as a custom HTTP Header.
                    //        new HeaderApiVersionReader("Accept-Version"),
                    //        // Use the following, if you would like to specify the version as a Media Type Header.
                    //        new MediaTypeApiVersionReader("api-version"));
                });

            return services;
        }
    }
}
