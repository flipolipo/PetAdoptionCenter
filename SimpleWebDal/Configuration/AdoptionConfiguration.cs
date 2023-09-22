using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.WebUser;


namespace SimpleWebDal.Configuration
{
    public class AdoptionConfiguration : IEntityTypeConfiguration<Adoption>
    {
        public void Configure(EntityTypeBuilder<Adoption> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.AdoptedPet)
                .WithOne()
                .HasForeignKey<Adoption>(a => a.PetId);

        }
    }
}
