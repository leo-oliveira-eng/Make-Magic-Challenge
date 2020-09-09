using Infrastructure.Repositories;
using Make.Magic.Challenge.Domain.House.Models;
using Make.Magic.Challenge.Domain.House.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Context;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Infra.Repositories
{
    public class HouseRepository : Repository<House, MakeMagicContext>, IHouseRepository
    {
        public HouseRepository(MakeMagicContext context) : base(context) { }

        public async Task<Maybe<House>> FindAsync(string externalId)
            => await DbSet.FirstOrDefaultAsync(house => house.ExternalId.Equals(externalId));
    }
}