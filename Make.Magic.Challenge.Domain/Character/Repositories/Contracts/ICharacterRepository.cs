using BaseEntity.Domain.Repositories;
using Make.Magic.Challenge.Domain.Character.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;

namespace Make.Magic.Challenge.Domain.Character.Repositories.Contracts
{
    public interface ICharacterRepository: IRepository<Models.Character> 
    {
        Task<List<CharacterModel>> FindAsync(GetCharactersDto dto);
    }
}
