using Api.Notification.Infrastructure.Middleware;
using Api.Notification.Infrastructure.Services.EventBus.Hubs;
using AspNetCore.Common.Infrastructure.Swagger;

namespace Api.Notification.Infrastructure.App
{
    /// <summary>
    /// App Bilder
    /// </summary>
    public static class AppBuilder
    {
        /// <summary>
        /// AppConfigure add all Use for app
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder AppConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHealthChecks("/health");

            app.UseSwaggerExtension();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication(); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationhub");
            });
            return app;
        }
    }
}
