using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.WebUser;
using System.Reflection.Emit;


namespace SimpleWebDal.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.BasicInformation)
                .WithOne()
                .HasForeignKey<User>(u => u.BasicInformationId);
          
            builder.HasOne(u => u.UserCalendar)
                .WithOne()
                .HasForeignKey<User>(u => u.UserCalendarId);
            builder.HasMany(u => u.Adoptions);
            builder.HasMany(u => u.Roles);
           builder.HasMany(e => e.Pets)
        .WithMany(e => e.Users)
          .UsingEntity<UserPet>(
            l => l.HasOne<Pet>().WithMany().HasForeignKey(e => e.PetId),
            r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId));
        }
    }
}
