using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using System;
using Model = Make.Magic.Challenge.Domain.Character.Models;
using Messages.Core;
using System.Threading.Tasks;
using Messages.Core.Extensions;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.House.Services.Contracts;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Domain.Character.Factories.Contracts;

namespace Make.Magic.Challenge.Domain.Character.Services
{
    public class CharacterService : ICharacterService
    {
        ICharacterRepository CharacterRepository { get; }

        IUnitOfWork Uow { get; }

        IHouseService HouseService { get; }

        ICharacterFactory CharacterFactory { get; }

        public CharacterService(ICharacterRepository characterRepository, IUnitOfWork uow, IHouseService houseService, ICharacterFactory charactereFactory)
        {
            CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
            HouseService = houseService ?? throw new ArgumentNullException(nameof(houseService));
            CharacterFactory = charactereFactory ?? throw new ArgumentNullException(nameof(charactereFactory));
        }

        public async Task<Response<Model.Character>> CreateAsync(CreateCharacterDto dto)
        {
            var response = Response<Model.Character>.Create();

            if (dto == null)
                return response.WithBusinessError("Character is invalid");

            var houseResponse = await HouseService.GetHouseAsync(dto.House);

            if (houseResponse.HasError)
                return response.WithMessages(houseResponse.Messages);

            return await CharacterFactory.CreateAsync(dto, houseResponse);
        }

        public async Task<Response<Model.Character>> UpdateAsync(UpdateCharacterDto dto)
        {
            var response = Response<Model.Character>.Create();

            if (dto == null)
                return response.WithBusinessError("Character is invalid");

            var character = await CharacterRepository.FindAsync(dto.Code);

            if (!character.HasValue)
                return response.WithBusinessError($"Character {dto.Code} not found");

            var getHouseResponse = await GetHouseAsync(dto.House, character.Value.House);

            if (getHouseResponse.HasError)
                return response.WithMessages(getHouseResponse.Messages);

            var updateResponse = character.Value.Update(dto.Name, dto.Role, dto.School, getHouseResponse, dto.Patronus);

            if (updateResponse.HasError)
                return response.WithMessages(updateResponse.Messages);

            await CharacterRepository.UpdateAsync(character);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Failed to try to update the character");

            return response.SetValue(character);
        }

        async Task<Response<CharacterHouse>> GetHouseAsync(string houseId, CharacterHouse characterHouse)
        {
            var response = Response<CharacterHouse>.Create();

            if (string.IsNullOrEmpty(houseId))
                return response.WithBusinessError($"{nameof(houseId)} is invalid");

            if (characterHouse.House.ExternalId.Equals(houseId))
                return response.SetValue(characterHouse);

            var getHouseResponse = await HouseService.GetHouseAsync(houseId);

            if (getHouseResponse.HasError)
                return response.WithMessages(getHouseResponse.Messages);

            return response.SetValue(new CharacterHouse(getHouseResponse));
        }
    }
}
