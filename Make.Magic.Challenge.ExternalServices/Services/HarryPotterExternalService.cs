using Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts;
using Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos;
using Make.Magic.Challenge.SharedKernel.Settings.Contracts;
using Messages.Core;
using Messages.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.ExternalServices.Services
{
    public class HarryPotterExternalService : IHarryPotterExternalService
    {
        HttpClient HttpClient { get; }

        IMagicSettings Settings { get; }

        public HarryPotterExternalService(IHttpClientFactory clientFactory, IMagicSettings settings)
        {
            if (clientFactory == null)
                throw new ArgumentNullException(nameof(clientFactory));

            HttpClient = clientFactory.CreateClient("HarryPotterHttpClientConfigurationName");
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<Response<HouseResponseDto>> GetHouseAsync(string houseId)
        {
            var response = Response<HouseResponseDto>.Create();

            if (string.IsNullOrEmpty(houseId))
                return response.WithBusinessError($"{houseId} is invalid");

            var url = $"{houseId}?key={Settings.HarryPotterAPIKey}";

            var houseResponse = await HttpClient.GetAsync(url);

            return DeserializeResponseAsync(houseResponse, response);
        }

        static Response<HouseResponseDto> DeserializeResponseAsync(HttpResponseMessage houseResponse, Response<HouseResponseDto> response)
        {
            if (!houseResponse.IsSuccessStatusCode)
                return response.WithCriticalError($"StatusCode: {houseResponse.StatusCode}; ReasonPhase: {houseResponse.ReasonPhrase}; RequestUri: {houseResponse.RequestMessage.RequestUri}");

            try
            {
                var content = houseResponse.Content.ReadAsStringAsync().Result;

                return response.SetValue(JsonConvert.DeserializeObject<List<HouseResponseDto>>(content)[0]);
            }
            catch (Exception ex)
            {
                return response.WithCriticalError(ex.ToString());
            }
        }
    }
}
