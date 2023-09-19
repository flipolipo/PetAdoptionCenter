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

    public DbSet<TempHouse> TempHouses { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }

    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<CalendarActivity> Calendars { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<BasicHealthInfo> BasicHealthInfos { get; set; }
    public DbSet<Adoption> AdoptionInfos { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PAC;Username=postgres;Password=Baskakisc98;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Address>()
           .HasKey(x => x.Id);

        builder.Entity<BasicInformation>()
          .HasKey(x => x.Id);

        builder.Entity<Credentials>()
     .HasKey(x => x.Id);

        builder.Entity<User>()
          .HasKey(x => x.Id);

        builder.Entity<ProfileModel>()
          .HasKey(x => x.Id);

        builder.Entity<CalendarActivity>()
          .HasKey(x => x.Id);

        builder.Entity<Activity>()
          .HasKey(x => x.Id);

        builder.Entity<Vaccination>()
    .HasKey(x => x.Id);

        builder.Entity<Disease>()
          .HasKey(x => x.Id);

        builder.Entity<BasicHealthInfo>()
         .HasKey(x => x.Id);

        builder.Entity<Pet>()
        .HasKey(x => x.Id);

        //creating Entities with unique Ids:

        builder.Entity<Address>()
            .HasIndex(u => u.Id)
            .IsUnique();

        builder.Entity<BasicInformation>()
            .HasIndex(u => u.Id)
            .IsUnique();

        builder.Entity<Credentials>()
           .HasIndex(u => u.Id)
           .IsUnique();

        builder.Entity<User>()
         .HasIndex(u => u.Id)
         .IsUnique();

        builder.Entity<ProfileModel>()
           .HasIndex(u => u.Id)
           .IsUnique();


        builder.Entity<CalendarActivity>()
          .HasIndex(u => u.Id)
          .IsUnique();

        builder.Entity<Activity>()
          .HasIndex(u => u.Id)
          .IsUnique();

        builder.Entity<Vaccination>()
      .HasIndex(u => u.Id)
      .IsUnique();

        builder.Entity<Disease>()
          .HasIndex(u => u.Id)
          .IsUnique();

        builder.Entity<BasicHealthInfo>()
          .HasIndex(u => u.Id)
          .IsUnique();

        builder.Entity<Pet>()
         .HasIndex(u => u.Id);
        

        //BasicInformation(parent) relations with Address (child) (one-to-one):

        builder.Entity<BasicInformation>()
        .HasOne(e => e.Address)
        .WithOne()
        .HasForeignKey<BasicInformation>(e => e.AddressId)
        .IsRequired();


        builder.Entity<CalendarActivity>()
        .HasMany(e => e.Activities)
        .WithOne()
        .HasForeignKey(e => e.Id)
        .IsRequired();

        builder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));
    
        builder.Entity<BasicHealthInfo>()
        .HasMany(e => e.Vaccinations)
        .WithOne()
        .HasForeignKey(e => e.Id)
        .IsRequired();

        builder.Entity<BasicHealthInfo>()
         .HasMany(e => e.MedicalHistory)
        .WithOne()
        .HasForeignKey(e => e.Id)
        .IsRequired();

        //  builder.Entity<Pet>()
        //.HasOne(e => e.BasicHealthInfo)
        //.WithOne()
        //.HasForeignKey<BasicHealthInfo>(e => e.PetRef)
        //.IsRequired();

        builder.Entity<Pet>()
        .HasMany(e => e.PatronUsers)
        .WithOne()
        .HasForeignKey(e => e.Id)
        .IsRequired();

        builder.Entity<TempHouse>()
        .HasKey(x => x.Id);

        builder.Entity<TempHouse>()
           .HasIndex(u => u.Id)
           .IsUnique();

        //TempHouse one-to-one

        builder.Entity<User>()
.HasOne(e => e.TempHouse)
.WithOne(a => a.TemporaryOwner)
.HasForeignKey<TempHouse>(a => a.UserId);

        builder.Entity<Address>()
        .HasOne(e => e.TemporaryHouse)
        .WithOne(a => a.TemporaryHouseAddress)
        .HasForeignKey<TempHouse>(a => a.AddressId);

        builder.Entity<Shelter>()
     .HasOne(e => e.TempHouse)
     .WithOne(a => a.ShelterName)
     .HasForeignKey<TempHouse>(a => a.ShelterId);

        //TempHouse one-to-many

        builder.Entity<TempHouse>()
         .HasMany(e => e.PetsInTemporaryHouse)
        .WithOne()
        .HasForeignKey(e => e.Id)
        .IsRequired();

        // Shelter:

        builder.Entity<Shelter>()
          .HasKey(x => x.Id);

        builder.Entity<Shelter>()
          .HasIndex(u => u.Id)
          .IsUnique();

        //Shelter one-to-one

        builder.Entity<Address>()
               .HasOne(e => e.Shelter)
               .WithOne(a => a.ShelterAddress)
               .HasForeignKey<Shelter>(a => a.AddressId);

        builder.Entity<CalendarActivity>()
                .HasOne(e => e.Shelter)
                .WithOne(a => a.ShelterCalendar)
                .HasForeignKey<Shelter>(a => a.CalendarActivityId);

        //Shelter one-to-many

        builder.Entity<Shelter>()
           .HasMany(e => e.ShelterUsers)
          .WithOne()
          .HasForeignKey(e => e.Id)
          .IsRequired();

        builder.Entity<Shelter>()
           .HasMany(e => e.ShelterPets)
          .WithOne()
          .HasForeignKey(e => e.Id)
          .IsRequired();

        builder.Entity<ProfileModel>()
        .HasMany(e => e.ProfilePets)
       .WithOne()
       .HasForeignKey(e => e.Id)
       .IsRequired();

        //builder.Entity<ProfileModel>()
        //     .HasOne(e => e.UserLogged)
        //     .WithOne()
        //     .HasForeignKey<User>(a => a.Id);

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