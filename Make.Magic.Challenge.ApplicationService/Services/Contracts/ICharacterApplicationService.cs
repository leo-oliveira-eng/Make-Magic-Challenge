using Make.Magic.Challenge.Messages.RequestMessages;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ApplicationService.Services.Contracts
{
    public interface ICharacterApplicationService
    {
        Task<Response<CharacterResponseMessage>> CreateAsync(CharacterRequestMessage requestMessage);

        Task<Response<CharacterResponseMessage>> GetCharacterAsync(Guid code);

        Task<Response<List<CharacterResponseMessage>>> GetCharacterAsync(GetCharactersRequestMessage requestMessage);

        Task<Response<CharacterResponseMessage>> UpdateAsync(Guid code, CharacterRequestMessage requestMessage);

        Task<Response<RemoveCharacterResponseMessage>> RemoveAsync(Guid code);
    }
}
