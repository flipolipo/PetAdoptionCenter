using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;

namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    //public DbSet<TempHouse> TempHouses { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }

    //public DbSet<Shelter> Shelters { get; set; }
    public DbSet<CalendarActivity> Calendars { get; set; }
    public DbSet<Activity> Activities { get; set; }
    //public DbSet<Vaccination> Vaccinations { get; set; }
    //public DbSet<Pet> Pets { get; set; }
    //public DbSet<Disease> Diseases { get; set; }
    //public DbSet<BasicHealthInfo> BasicHealthInfos { get; set; }
    //public DbSet<Adoption> AdoptionInfos { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PAC;Username=postgres;Password=Baskakisc98;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Address>()
           .HasKey(x => x.AddressId);

        builder.Entity<BasicInformation>()
          .HasKey(x => x.BasicInformationId);

        builder.Entity<Credentials>()
     .HasKey(x => x.CredentialsId);

        builder.Entity<User>()
          .HasKey(x => x.UserId);

        builder.Entity<ProfileModel>()
          .HasKey(x => x.ProfileId);

        builder.Entity<CalendarActivity>()
          .HasKey(x => x.CalendarId);

        builder.Entity<Activity>()
          .HasKey(x => x.ActivityId);

        //creating Entities with unique Ids:

        builder.Entity<Address>()
            .HasIndex(u => u.AddressId)
            .IsUnique();

        builder.Entity<BasicInformation>()
            .HasIndex(u => u.BasicInformationId)
            .IsUnique();

        builder.Entity<Credentials>()
           .HasIndex(u => u.CredentialsId)
           .IsUnique();

        builder.Entity<User>()
         .HasIndex(u => u.UserId)
         .IsUnique();

        builder.Entity<ProfileModel>()
           .HasIndex(u => u.ProfileId)
           .IsUnique();


        builder.Entity<CalendarActivity>()
          .HasIndex(u => u.CalendarId)
          .IsUnique();

        builder.Entity<Activity>()
          .HasIndex(u => u.ActivityId)
          .IsUnique();


        //BasicInformation(parent) relations with Address (child) (one-to-one):

        builder.Entity<BasicInformation>()
        .HasOne(e => e.Address)
        .WithOne()
        .HasForeignKey<BasicInformation>(e => e.AddressId)
        .IsRequired();


        builder.Entity<CalendarActivity>()
         .HasMany(e => e.Activities)
        .WithOne()
        .HasForeignKey(e => e.ActivityId)
        .IsRequired();

        builder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));
    

      

        //   //ProfileModel (parent) relation withPets (one-to-many):

        //   builder.Entity<ProfileModel>()
        //  .HasMany(e => e.ProfilePets)
        //    .WithOne();

        //   //ProfileModel (parent) relation UserLogged (one-to-one):

        //   builder.Entity<ProfileModel>()
        //  .HasOne(e => e.UserLogged)
        //    .WithOne();

       
    }
}