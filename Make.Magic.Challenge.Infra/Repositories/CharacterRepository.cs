using Infrastructure.Repositories;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Context;

namespace Make.Magic.Challenge.Infra.Repositories
{
    public class CharacterRepository : Repository<Character, MakeMagicContext>, ICharacterRepository
    {
        public CharacterRepository(MakeMagicContext context) : base(context) { }
    }
}
