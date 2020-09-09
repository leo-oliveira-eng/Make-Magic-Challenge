using Http.Request.Service.Middleware.Extensions;
using Make.Magic.Challenge.Connector.Services;
using Make.Magic.Challenge.Connector.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Make.Magic.Challenge.Connector.Middleware.Extensions
{
    public static class MakeMagicMiddlewareExtensions
    {
        public static IServiceCollection AddMakeMagicApiServiceConnector(this IServiceCollection services)
        {
            return services.AddHttpServices().AddTransient<IMakeMagicServiceConnector, MakeMagicServiceConnector>();
        }

        public static IApplicationBuilder UseMakeMagicApiConnector(this IApplicationBuilder app)
        {
            return app.UseHttpServices().UseMiddleware<MakeMagicMiddleware>();
        }
    }
}
