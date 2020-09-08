using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Messages.RequestMessages;
using Make.Magic.Challenge.Messages.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Make.Magic.Challenge.ApplicationService.Services.Mappers
{
    public static class CharacterMapper
    {
        public static CreateCharacterDto ToCreateCharacterDto(this CharacterRequestMessage requestMessage)
            => new CreateCharacterDto
            {
                Name = requestMessage.Name,
                House = requestMessage.House,
                Patronus = requestMessage.Patronus,
                Role = requestMessage.Role,
                School = requestMessage.School
            };

        public static CharacterResponseMessage ToCharacterResponseMessage(this Character character)
        {
            if (character == null)
                return new CharacterResponseMessage();

            return new CharacterResponseMessage
            {
                CharacterCode = character.Code,
                Name = character.Name,
                House = character.House.House.ExternalId,
                Patronus = character.Patronus,
                Role = character.Role,
                School = character.School
            };
        }

        public static GetCharactersDto ToGetCharactersDto(this GetCharactersRequestMessage requestMessage)
            => new GetCharactersDto
            {
                Name = requestMessage.Name,
                House = requestMessage.House,
                Patronus = requestMessage.Patronus,
                Role = requestMessage.Role,
                School = requestMessage.School
            };

        public static List<CharacterResponseMessage> ToListCharactersResponseMessage(this List<Character> characters)
            => characters.Select(character => new CharacterResponseMessage
            {
                CharacterCode = character.Code,
                Name = character.Name,
                House = character.House.House.ExternalId,
                Patronus = character.Patronus,
                Role = character.Role,
                School = character.School
            }).ToList();

        public static UpdateCharacterDto ToUpdateCharacterDto(this CharacterRequestMessage requestMessage, Guid code)
            => new UpdateCharacterDto
            {
                Code = code,
                Name = requestMessage.Name,
                House = requestMessage.House,
                Patronus = requestMessage.Patronus,
                Role = requestMessage.Role,
                School = requestMessage.School
            };
    }
}
