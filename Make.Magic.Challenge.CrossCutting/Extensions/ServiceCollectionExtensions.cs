using Microsoft.Extensions.DependencyInjection;

namespace Make.Magic.Challenge.CrossCutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencyInjector(this IServiceCollection services)
        {
            DependencyInjector.Configure(services);
        }
    }
}
