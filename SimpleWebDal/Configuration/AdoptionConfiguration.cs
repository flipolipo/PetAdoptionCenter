using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.AdoptionProccess;


namespace SimpleWebDal.Configuration
{
    public class AdoptionConfiguration : IEntityTypeConfiguration<Adoption>
    {
        public void Configure(EntityTypeBuilder<Adoption> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(u => u.Activity)
                   .WithOne()
                   .HasForeignKey<Adoption>(u => u.CalendarId);

        }
    }
}
