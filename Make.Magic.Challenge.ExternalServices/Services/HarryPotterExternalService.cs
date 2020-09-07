using Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos;
using Make.Magic.Challenge.SharedKernel.Settings.Contracts;
using Messages.Core;
using Messages.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ExternalServices.Services
{
    public class HarryPotterExternalService : IHarryPotterExternalService
    {
        IHttpClientFactory ClientFactory { get; }

        IMagicSettings Settings { get; }

        public HarryPotterExternalService(IHttpClientFactory clientFactory, IMagicSettings settings)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<Response<HouseResponseDto>> GetHouseAsync(string houseId)
        {
            var response = Response<HouseResponseDto>.Create();

            if (string.IsNullOrEmpty(houseId))
                return response.WithBusinessError($"{houseId} is invalid");

            var client = ClientFactory.CreateClient(Settings.HarryPotterAPIBaseUrl);

            var url = $"{houseId}?key={Settings.HarryPotterAPIKey}";

            var houseResponse = await client.GetAsync(url);

            return DeserializeResponseAsync(houseResponse, response);
        }

        static Response<HouseResponseDto> DeserializeResponseAsync(HttpResponseMessage houseResponse, Response<HouseResponseDto> response)
        {
            if (!houseResponse.IsSuccessStatusCode)
                return response.WithCriticalError($"StatusCode: {houseResponse.StatusCode}; ReasonPhase: {houseResponse.ReasonPhrase}; RequestUri: {houseResponse.RequestMessage.RequestUri}");

            try
            {
                var content = houseResponse.Content.ReadAsStringAsync().Result;

                return response.SetValue(JsonConvert.DeserializeObject<HouseResponseDto>(content));
            }
            catch (Exception ex)
            {
                return response.WithCriticalError(ex.ToString());
            }
        }
    }
}
