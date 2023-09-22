using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.WebUser;


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
            builder.HasOne(u => u.Credentials)
                .WithOne()
                .HasForeignKey<User>(a => a.CredentialsId);
            builder.HasOne(u => u.UserCalendar)
                .WithOne()
                .HasForeignKey<User>(u => u.UserCalendarId);
            builder.HasMany(u => u.Adoptions);
            builder.HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .UsingEntity(e => e.ToTable("UserRoles"));
        }
    }
}
