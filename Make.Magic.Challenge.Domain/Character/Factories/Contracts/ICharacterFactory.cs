using Make.Magic.Challenge.Domain.Character.Dtos;
using Messages.Core;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Character.Factories.Contracts
{
    public interface ICharacterFactory
    {
        Task<Response<CharacterModel>> CreateAsync(CreateCharacterDto dto, HouseModel house);
    }
}
