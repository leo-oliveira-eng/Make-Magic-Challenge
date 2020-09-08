using Make.Magic.Challenge.SharedKernel.ExternalServices.Dtos;
using Messages.Core;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.SharedKernel.ExternalServices.Contracts
{
    public interface IHarryPotterExternalService
    {
        Task<Response<HouseResponseDto>> GetHouseAsync(string houseId);
    }
}
