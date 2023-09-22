using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using System.Reflection;


namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }
    public DbSet<TempHouse> TempHouses { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Adoption> Adoptions { get; set; }
    public DbSet<BasicHealthInfo> BasicHealthInfos { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<CalendarActivity> CalendarActivities { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=9999;Database=PAC;Username=postgres;Password=filipple123;");
        base.OnConfiguring(optionsBuilder);
    }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}