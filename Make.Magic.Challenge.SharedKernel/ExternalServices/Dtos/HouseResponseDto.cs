using Newtonsoft.Json;

namespace Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos
{
    public class HouseResponseDto
    {
        [JsonProperty(propertyName: "_id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }
    }
}
