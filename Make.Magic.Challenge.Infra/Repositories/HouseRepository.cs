using Infrastructure.Repositories;
using Make.Magic.Challenge.Domain.House.Models;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Context;

namespace Make.Magic.Challenge.Infra.Repositories
{
    public class HouseRepository : Repository<House, MakeMagicContext>, IHouseRepository
    {
        public HouseRepository(MakeMagicContext context) : base(context) { }
    }
}