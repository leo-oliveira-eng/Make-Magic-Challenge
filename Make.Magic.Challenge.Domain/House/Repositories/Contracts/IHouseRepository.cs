using BaseEntity.Domain.Repositories;
using Messages.Core;
using System.Threading.Tasks;
using HouseModel = Make.Magic.Challenge.Domain.House.Models.House;

namespace Make.Magic.Challenge.Domain.House.Repositories.Contracts
{
    public interface IHouseRepository : IRepository<Models.House>
    {
        Task<Maybe<HouseModel>> FindAsync(string externalId);
    }
}
