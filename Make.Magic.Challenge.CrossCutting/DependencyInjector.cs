using BaseEntity.Domain.UnitOfWork;
using Infrastructure.UnitOfWork;
using Make.Magic.Challenge.ApplicationService.Services;
using Make.Magic.Challenge.ApplicationService.Services.Contracts;
using Make.Magic.Challenge.Domain.Character.Factories;
using Make.Magic.Challenge.Domain.Character.Factories.Contracts;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.Character.Services;
using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Domain.House.Services;
using Make.Magic.Challenge.Domain.House.Services.Contracts;
using Make.Magic.Challenge.ExternalServices.Services;
using Make.Magic.Challenge.Infra.Context;
using Make.Magic.Challenge.Infra.Repositories;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts;
using Make.Magic.Challenge.SharedKernel.Settings;
using Make.Magic.Challenge.SharedKernel.Settings.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Make.Magic.Challenge.CrossCutting
{
    public class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region ' Character '

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<ICharacterFactory, CharacterFactory>();
            services.AddTransient<ICharacterApplicationService, CharacterApplicationService>();

            #endregion

            #region ' House '

            services.AddTransient<IHouseRepository, HouseRepository>();
            services.AddTransient<IHouseService, HouseService>();

            #endregion

            #region ' Settings '

            services.AddSingleton<IMagicSettings>(sp => sp.GetRequiredService<IOptions<MagicSettings>>().Value);

            #endregion

            #region ' External Services '

            services.AddTransient<IHarryPotterExternalService, HarryPotterExternalService>();
            services.AddHttpClient<HarryPotterExternalService>();

            #endregion

            #region ' Unit of Work '

            services.AddScoped<IUnitOfWork, UnitOfWork<MakeMagicContext>>();

            #endregion
        }
    }
}
