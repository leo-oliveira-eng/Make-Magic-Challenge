using Make.Magic.Challenge.Domain.Character.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Make.Magic.Challenge.Infra.Mappings
{
    public class CharacterMapping : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable(nameof(Character));
        }
    }
}
