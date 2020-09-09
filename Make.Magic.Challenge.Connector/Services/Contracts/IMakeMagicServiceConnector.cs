using Make.Magic.Challenge.Messages.RequestMessages;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Connector.Services.Contracts
{
    public interface IMakeMagicServiceConnector
    {
        Task<Response<CharacterResponseMessage>> CreateCharacterAsync(CharacterRequestMessage requestMessage, string httpClientConfigurationName);

        Task<Response<CharacterResponseMessage>> GetCharacterAsync(Guid code, string httpClientConfigurationName);

        Task<Response<List<CharacterResponseMessage>>> GetCharacterAsync(GetCharactersRequestMessage requestMessage, string httpClientConfigurationName);

        Task<Response<CharacterResponseMessage>> UpdateCharacterAsync(Guid code, CharacterRequestMessage requestMessage, string httpClientConfigurationName);

        Task<Response> DeleteCharacterAsync(Guid code, string httpClientConfigurationName);
    }
}
