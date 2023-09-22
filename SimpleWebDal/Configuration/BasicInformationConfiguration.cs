

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Configuration
{
    public class BasicInformationConfiguration : IEntityTypeConfiguration<BasicInformation>
    {
        public void Configure(EntityTypeBuilder<BasicInformation> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b=>b.Address)
                .WithOne()
                .HasForeignKey<BasicInformation>(b=>b.AddressId);
        }
    }
}
