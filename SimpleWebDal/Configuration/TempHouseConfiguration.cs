using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.TemporaryHouse;

namespace SimpleWebDal.Configuration
{
    public class TempHouseConfiguration : IEntityTypeConfiguration<TempHouse>
    {
        public void Configure(EntityTypeBuilder<TempHouse> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.TemporaryHouseAddress)
                .WithOne()
                .HasForeignKey<TempHouse>(t => t.AddressId);
            builder.HasOne(t => t.TemporaryOwner)
            .WithOne()
            .HasForeignKey<TempHouse>(t => t.UserId);
            builder.HasMany(t => t.PetsInTemporaryHouse);

        }
    }
}
