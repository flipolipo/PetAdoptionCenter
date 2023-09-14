using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;



namespace SimpleWebDal.Data
{
    public class PetAdoptionCenterContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BasicInformation> BasicInformations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TempHouse> TempHouses { get; set; }
        public DbSet<Profile> Profiles { get; set; }
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
            var manager = new ConfigurationManager();
           
            optionsBuilder.UseNpgsql
                ("tutaj string!");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasIndex(u => u.Id)
                .IsUnique();

            builder.Entity<Address>()
                .HasData(
                    new Address { Id = 1, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                    new Address { Id = 2, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                    new Address { Id = 3, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                    new Address { Id = 4, City = "Gdynia", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" }

                );

        }
    }
}
