using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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

        public static void AddSwaggerGen(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Make Magic Challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Make Magic Challenge",
                        Email = "leo.oliveira.eng@outlook.com",
                        Url = new Uri("https://github.com/leo-oliveira-eng/Make-Magic-Challenge")
                    },
                    Version = "1.0.0"
                });
            });
        }
    }
}
