using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWebDal.Models.WebUser;



namespace SimpleWebDal.Data
{
    public class PetAdoptionCenterContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
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
