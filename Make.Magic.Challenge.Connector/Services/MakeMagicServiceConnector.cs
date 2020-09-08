using Http.Request.Service.Services.Contracts;
using Make.Magic.Challenge.Connector.Services.Contracts;
using Make.Magic.Challenge.Messages.RequestMessages;
using Make.Magic.Challenge.Messages.ResponseMessages;
using Messages.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Connector.Services
{
    public class MakeMagicServiceConnector : ServiceConnector, IMakeMagicServiceConnector
    {
        public MakeMagicServiceConnector(IHttpService httpService) : base(httpService, "character") { }

        public async Task<Response<CharacterResponseMessage>> CreateCharacterAsync(CharacterRequestMessage requestMessage, string httpClientConfigurationName)
           => await HttpService.PostAsync<CharacterRequestMessage, CharacterResponseMessage>(ResourceName, requestMessage, httpClientConfigurationName);

        public async Task<Response<CharacterResponseMessage>> GetCharacterAsync(Guid code, string httpClientConfigurationName)
            => await HttpService.GetAsync<CharacterResponseMessage>($"{ResourceName}/{code}", httpClientConfigurationName);

        public async Task<Response<List<CharacterResponseMessage>>> GetCharacterAsync(GetCharactersRequestMessage requestMessage, string httpClientConfigurationName)
        {
            if (requestMessage == null)
                requestMessage = new GetCharactersRequestMessage();

            return await HttpService.GetManyAsync<CharacterResponseMessage>(
                $"{ResourceName}?name={requestMessage.Name}&role={requestMessage.Role}&school={requestMessage.School}&house={requestMessage.House}&patronus={requestMessage.Patronus}", httpClientConfigurationName);
        }

        public async Task<Response<CharacterResponseMessage>> UpdateCharacterAsync(Guid code, CharacterRequestMessage requestMessage, string httpClientConfigurationName)
           => await HttpService.PutAsync<CharacterRequestMessage, CharacterResponseMessage>($"{ResourceName}/{code}", requestMessage, httpClientConfigurationName);

        public async Task<Response> DeleteCharacterAsync(Guid code, string httpClientConfigurationName)
            => await HttpService.DeleteAsync($"{ResourceName}/{code}", code, httpClientConfigurationName);
    }
}
