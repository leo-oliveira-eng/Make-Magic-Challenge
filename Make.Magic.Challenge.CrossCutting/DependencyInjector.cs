using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
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

            #endregion
        }
    }
}
