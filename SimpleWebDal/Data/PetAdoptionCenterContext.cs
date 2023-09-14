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
    public DbSet<Role> Roles { get; set; }
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
        optionsBuilder.UseNpgsql("Wasz string");
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


        //Address(child) relations with parents (one-to-one):

        builder.Entity<BasicInformation>()
         .HasOne(e => e.Address)
         .WithOne(e => e.BasicInformation)
         .HasForeignKey<Address>(a => a.BasicInformationId);

     





        //User (parent) relation with BasicInformation and Calendar (childs) (one-to-one):

        builder.Entity<User>()
         .HasOne(e => e.BasicInformation)
         .WithOne(e => e.User)
         .HasForeignKey<BasicInformation>(a => a.UserId);

        builder.Entity<User>()
        .HasOne(e => e.UserCalendar)
        .WithOne(e => e.User)
        .HasForeignKey<CalendarModelClass>(a => a.UserId);

        //User (parent) relation with many Roles (child) (one-to-many):

        builder.Entity<User>()
        .HasMany(e => e.Roles)
          .WithOne(e => e.User)
          .HasForeignKey(e => e.UserId)
          .HasPrincipalKey(e => e.UserId);

        //TempHouse (parent) relation with User, Address, Shelter (childs) (one-to-one):

        builder.Entity<TempHouse>()
         .HasOne(e => e.TemporaryOwner)
         .WithOne(e => e.TempHouse)
         .HasForeignKey<User>(a => a.UserId);

        builder.Entity<TempHouse>()
        .HasOne(e => e.TemporaryHouseAddress)
        .WithOne(e => e.TempHouse)
        .HasForeignKey<Address>(a => a.TempHouseId);

        builder.Entity<TempHouse>()
        .HasOne(e => e.ShelterName)
        .WithOne(e => e.TempHouse)
        .HasForeignKey<Shelter>(a => a.TempHouseId);

        //TempHouse (parent) relation with PetsInTemporaryHouse (childs) (one-to-many):

        builder.Entity<TempHouse>()
       .HasMany(e => e.PetsInTemporaryHouse)
         .WithOne(e => e.TempHouse)
         .HasForeignKey(e => e.TempHouseId)
         .HasPrincipalKey(e => e.TempHouseId);

        //Profile (parent) relation with FavouriteListPets, VirtualAdoptionPetsList (childs) (one-to-many):

        builder.Entity<ProfileModel>()
       .HasMany(e => e.FavouriteListPets)
         .WithOne(e => e.Profile)
         .HasForeignKey(e => e.ProfileId)
         .HasPrincipalKey(e => e.ProfileId);

        builder.Entity<ProfileModel>()
       .HasMany(e => e.VirtualAdoptionPetsList)
         .WithOne(e => e.ProfileForVirtualAdoption)
         .HasForeignKey(e => e.ProfileIdForVirtualAdoptionId)
         .HasPrincipalKey(e => e.ProfileId);

        //Shelter (parent) relation with ListOfPetsAdopted, ListOfPets, ShelterWorkers, ShelterContributors (childs) (one-to-many):

        builder.Entity<Shelter>()
       .HasMany(e => e.ListOfPetsAdopted)
         .WithOne(e => e.ShelterListOfPetsAdopted)
         .HasForeignKey(e => e.ShelterListOfPetsAdoptedId)
         .HasPrincipalKey(e => e.ShelterId);

        builder.Entity<Shelter>()
       .HasMany(e => e.ListOfPets)
         .WithOne(e => e.ShelterListOfPets)
         .HasForeignKey(e => e.ShelterListOfPetsId)
         .HasPrincipalKey(e => e.ShelterId);

        builder.Entity<Shelter>()
       .HasMany(e => e.ShelterWorkers)
         .WithOne(e => e.ShelterWorkers)
         .HasForeignKey(e => e.ShelterWorkersId)
         .HasPrincipalKey(e => e.ShelterId);

        builder.Entity<Shelter>()
       .HasMany(e => e.ShelterContributors)
         .WithOne(e => e.ShelterContributors)
         .HasForeignKey(e => e.ShelterContributorsId)
         .HasPrincipalKey(e => e.ShelterId);

        //Shelter (parent) relation with User, Address, Calendar (childs) (one-to-one):

        builder.Entity<Shelter>()
          .HasOne(e => e.ShelterOwner)
          .WithOne(e => e.Shelter)
          .HasForeignKey<User>(a => a.ShelterId);

        builder.Entity<Shelter>()
     .HasOne(e => e.ShelterAddress)
     .WithOne(e => e.Shelter)
     .HasForeignKey<Address>(a => a.ShelterId);

        builder.Entity<Shelter>()
        .HasOne(e => e.ShelterCalendar)
        .WithOne(e => e.Shelter)
        .HasForeignKey<CalendarModelClass>(a => a.ShelterId);

        //Calendar (parent) relation with Activities (childs) (one-to-many):

        builder.Entity<CalendarModelClass>()
       .HasMany(e => e.Activities)
         .WithOne(e => e.Calendar)
         .HasForeignKey(e => e.CalendarId)
         .HasPrincipalKey(e => e.CalendarId);

        //Activity (parent) relation with Pet (child) (one-to-one):

        builder.Entity<Activity>()
         .HasOne(e => e.Pet)
         .WithOne(e => e.Activity)
         .HasForeignKey<Pet>(a => a.ActivityId);

        //BasicHealthInfo (parent) relation with Diseases and Vaccinations (childs) (one-to-many):

        builder.Entity<BasicHealthInfo>()
       .HasMany(e => e.Vaccinations)
         .WithOne(e => e.BasicHealthInfo)
         .HasForeignKey(e => e.BasicHealthInfoId)
         .HasPrincipalKey(e => e.BasicHealthInfoId);

        builder.Entity<BasicHealthInfo>()
      .HasMany(e => e.MedicalHistory)
        .WithOne(e => e.BasicHealthInfo)
        .HasForeignKey(e => e.BasicHealthInfoId)
        .HasPrincipalKey(e => e.BasicHealthInfoId);

        //Pet (parent) relation with BasicHealthInfo, Callendar, Shelter (child) (one-to-one):

        builder.Entity<Pet>()
         .HasOne(e => e.BasicHealthInfo)
         .WithOne(e => e.Pet)
         .HasForeignKey<BasicHealthInfo>(a => a.PetId);

        builder.Entity<Pet>()
        .HasOne(e => e.Callendar)
        .WithOne(e => e.Pet)
        .HasForeignKey<CalendarModelClass>(a => a.PetId);

        builder.Entity<Pet>()
        .HasOne(e => e.Shelter)
        .WithOne(e => e.Pet)
        .HasForeignKey<Shelter>(a => a.PetId);

        //Pet (parent) relation with PatronUsers (childs) (one-to-many):

        builder.Entity<Pet>()
       .HasMany(e => e.PatronUsers)
         .WithOne(e => e.Pet)
         .HasForeignKey(e => e.PetId)
         .HasPrincipalKey(e => e.PetId);

        //Adoption (parent) relation with User, Pet, Shelter (child) (one-to-one):

        builder.Entity<Adoption>()
         .HasOne(e => e.Adopter)
         .WithOne(e => e.Adoption)
         .HasForeignKey<User>(a => a.AdoptionId);

        builder.Entity<Adoption>()
       .HasOne(e => e.PetToAdoption)
       .WithOne(e => e.Adoption)
       .HasForeignKey<Pet>(a => a.AdoptionId);

        builder.Entity<Adoption>()
       .HasOne(e => e.Shelter)
       .WithOne(e => e.Adoption)
       .HasForeignKey<Shelter>(a => a.AdoptionId);

    }
}