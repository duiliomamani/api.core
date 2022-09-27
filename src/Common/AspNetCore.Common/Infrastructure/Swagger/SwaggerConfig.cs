using AspNetCore.Common.Statics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AspNetCore.Common.Infrastructure.Swagger
{
    public static class SwaggerConfig
    {
        /// <summary>
        /// Add SwaggerExtension With Schema Security JWT Bearer Token
        /// </summary>
        /// <param name="services"></param>
        /// <see href="https://aka.ms/aspnetcore/swashbuckle"/>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            var swagger = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings.Swagger>>().Value;

            services.AddSwaggerGen(config =>
            {
                config.CustomSchemaIds(x => x.FullName);

                var apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    config.SwaggerDoc(
                        $"ApiSwagger{description.GroupName}",
                        new OpenApiInfo()
                        {
                            Title = swagger.Title,
                            Version = description.ApiVersion.ToString(),
                            //Contact = null,
                            //TermsOfService = null
                        });
                }

                config.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor.GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                        $"ApiSwaggerv{v}" == documentName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"ApiSwaggerv{v}" == documentName);
                });

                if (swagger.DocumentationApi)
                {
                    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    config.IncludeXmlComments(xmlPath);
                }
                OpenApiSecurityScheme securityDefinition = new()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                OpenApiSecurityRequirement securityRequirements = new()
                {
                    {
                        securityDefinition,
                        Array.Empty<string>()
                    }
                };

                config.AddSecurityDefinition("Bearer", securityDefinition);

                // Make sure swagger UI requires a Bearer token to be specified
                config.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }

        /// <summary>
        /// Add Swagger Extension in App
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            var swagger = app.ApplicationServices.GetRequiredService<IOptions<AppSettings.Swagger>>().Value;

            if (swagger.Enabled)
            {
                // Register the Swagger generator and the Swagger UI middlewares
                app.UseSwagger();

                app.UseSwaggerUI(config =>
                {
                    var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                    config.DocumentTitle = swagger.Title;

                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        config.SwaggerEndpoint($"/swagger/" +
                            $"ApiSwagger{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                    config.RoutePrefix = swagger.RoutePrefix;
                });
            }
        }
    }
}
