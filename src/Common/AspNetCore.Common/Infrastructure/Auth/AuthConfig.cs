using AspNetCore.Common.Statics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AspNetCore.Common.Infrastructure.Auth
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthExtension(this IServiceCollection services)
        {
            var auth0 = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings.Auth>>().Value;

            services.AddAuthentication(options =>
                    {
                        //Set default Authentication Schema as Bearer
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.Authority = auth0.Domain;
                        options.Audience = auth0.Audience;
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            //NameClaimType = ClaimTypes.NameIdentifier,
                            ValidIssuer = auth0.Domain,
                            ValidAudience = auth0.Audience,
                            ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                            ValidateIssuer = true,
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = true,
                            ValidateLifetime = true
                        };
                    });

            return services;
        }
    }
}
