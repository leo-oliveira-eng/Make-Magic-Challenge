using Make.Magic.Challenge.Domain.Character.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Make.Magic.Challenge.Infra.Mappings
{
    public class CharacterHouseMapping : IEntityTypeConfiguration<CharacterHouse>
    {
        public void Configure(EntityTypeBuilder<CharacterHouse> builder)
        {
            builder.ToTable(nameof(CharacterHouse));

            builder.HasOne(x => x.House).WithMany().HasForeignKey(x => x.HouseId);
        }
    }
}
