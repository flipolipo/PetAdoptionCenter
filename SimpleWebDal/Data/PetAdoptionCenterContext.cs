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
  
    public DbSet<User> Users { get; set; }
    public DbSet<TempHouse> TempHouses { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }

    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<CalendarModelClass> Calendars { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<BasicHealthInfo> BasicHealthInfos { get; set; }
    public DbSet<Adoption> AdoptionInfos { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PAC;Username=postgres;Password=rudy102;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Address>()
           .HasKey(x => x.AddressId);

        builder.Entity<BasicInformation>()
          .HasKey(x => x.BasicInformationId);

        builder.Entity<User>()
          .HasKey(x => x.UserId);

        builder.Entity<TempHouse>()
          .HasKey(x => x.TempHouseId);

        builder.Entity<ProfileModel>()
          .HasKey(x => x.ProfileId);

        builder.Entity<Shelter>()
          .HasKey(x => x.ShelterId);

        builder.Entity<CalendarModelClass>()
          .HasKey(x => x.CalendarId);

        builder.Entity<Activity>()
          .HasKey(x => x.ActivityId);

        builder.Entity<Vaccination>()
          .HasKey(x => x.VaccinationId);

        builder.Entity<Pet>()
          .HasKey(x => x.PetId);

        builder.Entity<Disease>()
.HasKey(u => u.DiseaseId);


        builder.Entity<BasicHealthInfo>()
  .HasKey(u => u.BasicHealthInfoId);


        builder.Entity<Adoption>()
  .HasKey(u => u.AdoptionId);


        //creating Entities with unique Ids:

        builder.Entity<Address>()
            .HasIndex(u => u.AddressId)
            .IsUnique();

        builder.Entity<BasicInformation>()
            .HasIndex(u => u.BasicInformationId)
            .IsUnique();

        builder.Entity<User>()
           .HasIndex(u => u.UserId)
           .IsUnique();

        builder.Entity<TempHouse>()
           .HasIndex(u => u.TempHouseId)
           .IsUnique();

        builder.Entity<ProfileModel>()
           .HasIndex(u => u.ProfileId)
           .IsUnique();

        builder.Entity<Shelter>()
          .HasIndex(u => u.ShelterId)
          .IsUnique();

        builder.Entity<CalendarModelClass>()
          .HasIndex(u => u.CalendarId)
          .IsUnique();

        builder.Entity<Activity>()
          .HasIndex(u => u.ActivityId)
          .IsUnique();

        builder.Entity<Vaccination>()
         .HasIndex(u => u.VaccinationId)
         .IsUnique();

        builder.Entity<Pet>()
      .HasIndex(u => u.PetId)
      .IsUnique();

        builder.Entity<Disease>()
  .HasIndex(u => u.DiseaseId)
  .IsUnique();

        builder.Entity<BasicHealthInfo>()
  .HasIndex(u => u.BasicHealthInfoId)
  .IsUnique();

        builder.Entity<Adoption>()
  .HasIndex(u => u.AdoptionId)
  .IsUnique();


        //BasicInformation(parent) relations with Address (child) (one-to-one):

        builder.Entity<BasicInformation>()
         .HasOne(e => e.Address)
         .WithOne();


        //User (parent) relation with BasicInformation and Calendar (childs) (one-to-one):

        builder.Entity<User>()
         .HasOne(e => e.BasicInformation)
         .WithOne();

        builder.Entity<User>()
        .HasOne(e => e.UserCalendar)
        .WithOne();


        //TempHouse (parent) relation with User, Address, Shelter (childs) (one-to-one):

        builder.Entity<TempHouse>()
         .HasOne(e => e.TemporaryOwner)
         .WithOne();

        builder.Entity<TempHouse>()
        .HasOne(e => e.TemporaryHouseAddress)
        .WithOne();

        builder.Entity<TempHouse>()
        .HasOne(e => e.ShelterName)
        .WithOne();

        //TempHouse (parent) relation with PetsInTemporaryHouse (childs) (one-to-many):

        builder.Entity<TempHouse>()
       .HasMany(e => e.PetsInTemporaryHouse)
         .WithOne();

        //ProfileModel (parent) relation withPets (one-to-many):

        builder.Entity<ProfileModel>()
       .HasMany(e => e.ProfilePets)
         .WithOne();

        //ProfileModel (parent) relation UserLogged (one-to-one):

        builder.Entity<ProfileModel>()
       .HasOne(e => e.UserLogged)
         .WithOne();

        //Shelter (parent) relation with Pets and Users (childs) (one-to-many):



        builder.Entity<Shelter>()
       .HasMany(e => e.ShelterPets)
         .WithOne();

        builder.Entity<Shelter>()
       .HasMany(e => e.ShelterUsers)
         .WithOne();

        

        //Shelter (parent) relation with User, Address, Calendar (childs) (one-to-one):



        builder.Entity<Shelter>()
     .HasOne(e => e.ShelterAddress)
     .WithOne();


        builder.Entity<Shelter>()
        .HasOne(e => e.ShelterCalendar)
        .WithOne();

        //Calendar (parent) relation with Activities (childs) (one-to-many):

        builder.Entity<CalendarModelClass>()
       .HasMany(e => e.Activities)
         .WithOne();

        //Activity (parent) relation with Pet (child) (one-to-one):

        builder.Entity<Activity>()
         .HasOne(e => e.Pet)
         .WithOne();

        //BasicHealthInfo (parent) relation with Diseases and Vaccinations (childs) (one-to-many):

        builder.Entity<BasicHealthInfo>()
       .HasMany(e => e.Vaccinations)
         .WithOne();


        builder.Entity<BasicHealthInfo>()
      .HasMany(e => e.MedicalHistory)
        .WithOne();


        //Pet (parent) relation with BasicHealthInfo, Callendar, Shelter (child) (one-to-one):

        builder.Entity<Pet>()
         .HasOne(e => e.BasicHealthInfo)
         .WithOne();

        builder.Entity<Pet>()
        .HasOne(e => e.Callendar)
        .WithOne();

       

        //Pet (parent) relation with PatronUsers (childs) (one-to-many):

        builder.Entity<Pet>()
       .HasMany(e => e.PatronUsers)
         .WithOne();

        //Adoption (parent) relation with User, Pet, Shelter (child) (one-to-one):

        builder.Entity<Adoption>()
         .HasOne(e => e.Adopter)
         .WithOne();

        builder.Entity<Adoption>()
       .HasOne(e => e.AdoptedPet)
       .WithOne();

        builder.Entity<Adoption>()
       .HasOne(e => e.Shelter)
       .WithOne();

    }
}