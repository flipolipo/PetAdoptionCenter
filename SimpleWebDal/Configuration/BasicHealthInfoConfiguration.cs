using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.Animal;


namespace SimpleWebDal.Configuration
{
    public class BasicHealthInfoConfiguration : IEntityTypeConfiguration<BasicHealthInfo>
    {
        public void Configure(EntityTypeBuilder<BasicHealthInfo> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.Vaccinations);
            builder.HasMany(b => b.MedicalHistory);
        }
    }
}
