using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{

    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }

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



    public PetAdoptionCenterContext(DbContextOptions<PetAdoptionCenterContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

   

}
