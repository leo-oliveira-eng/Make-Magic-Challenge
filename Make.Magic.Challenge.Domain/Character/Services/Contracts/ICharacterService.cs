using Make.Magic.Challenge.Domain.Character.Dtos;
using Messages.Core;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;

namespace Make.Magic.Challenge.Domain.Character.Services.Contracts
{
    public interface ICharacterService
    {
        Task<Response<CharacterModel>> CreateAsync(CreateCharacterDto dto);

        Task<Response<CharacterModel>> UpdateAsync(UpdateCharacterDto dto);
    }
}
