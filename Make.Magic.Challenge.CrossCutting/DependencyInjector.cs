using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
//using Make.Magic.Challenge.Domain.Character.Services;
//using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Repositories;
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
            //services.AddTransient<ICharacterService, CharacterService>();

            #endregion

            #region ' House '

            services.AddTransient<IHouseRepository, HouseRepository>();

            #endregion

            #region ' Settings '

            services.AddSingleton<IMagicSettings>(sp => sp.GetRequiredService<IOptions<MagicSettings>>().Value);

            #endregion
        }
    }
}
