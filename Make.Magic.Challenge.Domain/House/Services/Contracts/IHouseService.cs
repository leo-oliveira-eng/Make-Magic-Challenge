using Messages.Core;
using System.Threading.Tasks;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.House.Services.Contracts
{
    public interface IHouseService
    {
        Task<Response<HouseModel>> GetHouseAsync(string houseId);
    }
}
