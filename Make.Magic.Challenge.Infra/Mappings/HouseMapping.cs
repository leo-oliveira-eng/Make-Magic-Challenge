using Make.Magic.Challenge.Domain.House.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Make.Magic.Challenge.Infra.Mappings
{
    public class HouseMapping : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.ToTable(nameof(House));
        }
    }
}
