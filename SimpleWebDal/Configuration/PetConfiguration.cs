using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.Animal;

namespace SimpleWebDal.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Users);
            builder.HasOne(p => p.Calendar)
                     .WithOne()
                     .HasForeignKey<Pet>(p => p.CalendarId);
            builder.HasOne(p => p.BasicHealthInfo)
                   .WithOne()
                   .HasForeignKey<Pet>(p => p.BasicHealthInfoId);
        }

       

    }
}
