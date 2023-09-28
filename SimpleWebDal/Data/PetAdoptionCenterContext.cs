using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TempHouse> TempHouses { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Adoption> Adoptions { get; set; }
    public DbSet<BasicHealthInfo> BasicHealthInfos { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<CalendarActivity> CalendarActivities { get; set; }
    public DbSet<PatronUsers> PatronsUsers { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SimpleWebDal"))
     .AddJsonFile("secrets.json")
     .Build();

        //Do sprawdzenia czy widzi secret.json
        //Console.WriteLine($"Full JSON: {configuration.GetDebugView()}");

        var connectionString = configuration["ConnectionStrings:MyConnection"];

        //Do sprawdzenia czy widzi ConnectonString
        //Console.WriteLine($"Connection String: {connectionString}");

        optionsBuilder.UseNpgsql(connectionString);
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}