using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.ProfileUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebDal.Configuration
{
    public class ProfileModelConfiguration : IEntityTypeConfiguration<ProfileModel>
    {
        public void Configure(EntityTypeBuilder<ProfileModel> builder)
        {
            builder.HasKey(pm => pm.Id);
            builder.HasMany(pm => pm.ProfilePets);
            builder.HasOne(pm => pm.UserLogged)
                .WithOne()
                .HasForeignKey<ProfileModel>(p => p.UserId);
            builder.HasMany(pm => pm.ProfilePets)
              .WithMany(p => p.Profiles)
              .UsingEntity(j => j.ToTable("ProfilePets"));

        }
    }
}
