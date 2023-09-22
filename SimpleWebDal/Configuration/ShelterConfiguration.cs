using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;


namespace SimpleWebDal.Configuration
{
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
        public void Configure(EntityTypeBuilder<Shelter> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.ShelterAddress)
                .WithOne()
                .HasForeignKey<Shelter>(s => s.AddressId);
            builder.HasOne(s => s.ShelterCalendar)
                .WithOne()
                .HasForeignKey<Shelter>(a => a.CalendarId);
            builder.HasMany(s => s.ShelterUsers);
            builder.HasMany(s => s.ShelterPets);
            builder.HasMany(s => s.Adoptions);
            builder.HasMany(s => s.TempHouses);

        }
    }
}
