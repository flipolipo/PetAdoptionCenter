using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebDal.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.PatronUsers);
            builder.HasOne(p => p.Calendar)
                     .WithOne()
                     .HasForeignKey<Pet>(p => p.CalendarId);
            builder.HasOne(p => p.BasicHealthInfo)
                   .WithOne()
                   .HasForeignKey<Pet>(p => p.BasicHealthInfoId);
        }

       

    }
}
