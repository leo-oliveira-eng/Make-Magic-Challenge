using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Make.Magic.Challenge.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPolly(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("HarryPotterHttpClientConfigurationName", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("HarryPotterAPIBaseUrl").Value);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
