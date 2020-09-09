using Infrastructure.Repositories;
using Make.Magic.Challenge.Domain.Character.Dtos;
using Make.Magic.Challenge.Domain.Character.Models;
using Make.Magic.Challenge.Domain.Character.Repositories.Contracts;
using Make.Magic.Challenge.Infra.Context;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make.Magic.Challenge.Infra.Repositories
{
    public class CharacterRepository : Repository<Character, MakeMagicContext>, ICharacterRepository
    {
        public CharacterRepository(MakeMagicContext context) : base(context) { }

        public async Task<List<Character>> FindAsNoTrackingAsync(GetCharactersDto dto)
            => await DbSet.AsNoTracking()
                .Include(x => x.House)
                    .ThenInclude(x => x.House)
                .Where(character =>
                    (
                        string.IsNullOrEmpty(dto.Name) 
                        || character.Name.StartsWith(dto.Name, StringComparison.OrdinalIgnoreCase) 
                        || character.Name.EndsWith(dto.Name, StringComparison.OrdinalIgnoreCase)
                    )
                        &&
                    (
                        string.IsNullOrEmpty(dto.House) 
                        || character.House.House.ExternalId.StartsWith(dto.House, StringComparison.OrdinalIgnoreCase) 
                        || character.House.House.ExternalId.EndsWith(dto.House, StringComparison.OrdinalIgnoreCase)
                    )
                        &&

                    (
                        string.IsNullOrEmpty(dto.Patronus) 
                        || !string.IsNullOrEmpty(character.Patronus) && character.Patronus.StartsWith(dto.Patronus, StringComparison.OrdinalIgnoreCase)
                        || !string.IsNullOrEmpty(character.Patronus) && character.Patronus.EndsWith(dto.Patronus, StringComparison.OrdinalIgnoreCase)
                    )
                        &&
                    (
                        string.IsNullOrEmpty(dto.Role)
                        || character.Role.StartsWith(dto.Role, StringComparison.OrdinalIgnoreCase)
                        || character.Role.EndsWith(dto.Role, StringComparison.OrdinalIgnoreCase)
                    )
                        &&
                    (
                        string.IsNullOrEmpty(dto.School)
                        || character.School.StartsWith(dto.School, StringComparison.OrdinalIgnoreCase)
                        || character.School.EndsWith(dto.School, StringComparison.OrdinalIgnoreCase)
                    ))
                .ToListAsync();

        public async Task<Maybe<Character>> FindAsNoTrackingAsync(Guid code)
            => await DbSet.AsNoTracking()
                .Include(x => x.House)
                    .ThenInclude(x => x.House)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));

        public async override Task<Maybe<Character>> FindAsync(Guid code)
            => await DbSet.AsNoTracking()
                .Include(x => x.House)
                    .ThenInclude(x => x.House)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
    }
}
