using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Make.Magic.Challenge.Api.Extensions;
using Make.Magic.Challenge.CrossCutting.Extensions;
using Make.Magic.Challenge.Infra.Context;
using Make.Magic.Challenge.SharedKernel.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Make.Magic.Challenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MakeMagicContext>(options => options.UseMySql(Configuration.GetConnectionString("MakeMagic")));

            services.AddMvc();

            services.AddHttpContextAccessor();

            services.Configure<MagicSettings>(Configuration.GetSection(nameof(MagicSettings)));

            services.ConfigureDependencyInjector();

            services.AddPolly(Configuration);

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Make Magic Challenge");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
