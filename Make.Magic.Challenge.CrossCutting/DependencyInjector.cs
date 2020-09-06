using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.Character.Services;
using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}
