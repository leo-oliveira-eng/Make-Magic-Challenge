using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Factories.Contracts;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;
using CharacterModel = Make.Magic.Challenge.Domain.Character.Models.Character;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.Character.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        IUnitOfWork Uow { get; }

        ICharacterRepository CharacterRepository { get; }

        public CharacterFactory(IUnitOfWork uow, ICharacterRepository characterRepository)
        {
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
            CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
        }

        public async Task<Response<CharacterModel>> CreateAsync(CreateCharacterDto dto, HouseModel house)
        {
            var response = Response<CharacterModel>.Create();

            var characterHouse = new CharacterHouse(house);

            var createCharacterResponse = CharacterModel.Create(dto.Name, dto.Role, dto.School, characterHouse, dto.Patronus);

            if (createCharacterResponse.HasError)
                return response.WithMessages(createCharacterResponse.Messages);

            await CharacterRepository.AddAsync(createCharacterResponse);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Failed to try to save the character");

            return response.SetValue(createCharacterResponse);
        }
    }
}
