using BaseEntity.Domain.UnitOfWork;
using Make.Magic.Challenge.ApplicationService.Services.Contracts;
using Make.Magic.Challenge.ApplicationService.Services.Mappers;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Domain.Character.Services.Contracts;
using Make.Magic.Challenge.Messages.RequestMessages;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Services
{
    public class CharacterApplicationService : ICharacterApplicationService
    {
        ICharacterRepository CharacterRepository { get; }

        ICharacterService CharacterService { get; }

        IUnitOfWork Uow { get; }

        public CharacterApplicationService(ICharacterRepository characterRepository, ICharacterService characterService, IUnitOfWork uow)
        {
            CharacterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
            CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<CharacterResponseMessage>> CreateAsync(CharacterRequestMessage requestMessage)
        {
            var response = Response<CharacterResponseMessage>.Create();

            if (requestMessage == null)
                return response.WithBusinessError("Invalid data for character creation");

            var createCharacterResponse = await CharacterService.CreateAsync(requestMessage.ToCreateCharacterDto());

            if (createCharacterResponse.HasError)
                return response.WithMessages(createCharacterResponse.Messages);

            return response.SetValue(createCharacterResponse.Data.Value.ToCharacterResponseMessage());
        }

        public async Task<Response<CharacterResponseMessage>> GetCharacterAsync(Guid code)
        {
            var response = Response<CharacterResponseMessage>.Create();

            if (code.Equals(Guid.Empty))
                return response.WithBusinessError($"{nameof(code)} is invalid");

            var character = await CharacterRepository.FindAsNoTrackingAsync(code);

            if (!character.HasValue)
                return response.WithBusinessError($"Character with code {code} not found");

            return response.SetValue(character.Value.ToCharacterResponseMessage());
        }

        public async Task<Response<List<CharacterResponseMessage>>> GetCharacterAsync(GetCharactersRequestMessage requestMessage)
        {
            var response = Response<List<CharacterResponseMessage>>.Create();

            if (requestMessage == null)
                return response.WithBusinessError("Invalid data for search character");

            var characters = await CharacterRepository.FindAsNoTrackingAsync(requestMessage.ToGetCharactersDto());

            if (!characters.Any())
                return response.WithBusinessError("No characters with the requested characteristics.");

            return response.SetValue(characters.ToListCharactersResponseMessage());
        }

        public async Task<Response<CharacterResponseMessage>> UpdateAsync(Guid code, CharacterRequestMessage requestMessage)
        {
            var response = Response<CharacterResponseMessage>.Create();

            if (requestMessage == null)
                return response.WithBusinessError("Invalid data for update character");

            var updateResponse = await CharacterService.UpdateAsync(requestMessage.ToUpdateCharacterDto(code));

            if (updateResponse.HasError)
                return response.WithMessages(updateResponse.Messages);

            return response.SetValue(updateResponse.Data.Value.ToCharacterResponseMessage());
        }

        public async Task<Response<RemoveCharacterResponseMessage>> RemoveAsync(Guid code)
        {
            var response = Response<RemoveCharacterResponseMessage>.Create();

            if (code.Equals(Guid.Empty))
                return response.WithBusinessError($"{nameof(code)} is invalid");

            var character = await CharacterRepository.FindAsync(code);

            if (!character.HasValue)
                return response.WithBusinessError($"Character with code {code} not found");

            character.Value.Delete();

            await CharacterRepository.UpdateAsync(character);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Failed to try to delete the character");

            return response;
        }
    }
}
