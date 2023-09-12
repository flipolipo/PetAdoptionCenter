using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWebDal.Models.WebUser;



namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PAC;Username=postgres;Password=Baskakisc98;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Address>()
            .HasIndex(u => u.AddressId)
            .IsUnique();

        builder.Entity<Address>()
            .HasData(
                new Address { AddressId = 1, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                new Address { AddressId = 2, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                new Address { AddressId = 3, City = "Warsaw", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" },
                new Address { AddressId = 4, City = "Gdynia", FlatNumber = 3, HouseNumber = "3a", PostalCode = "48-456", Street = "Janasa" }
            );

        builder.Entity<BasicInformation>()
            .HasIndex(u => u.BasicInformationId)
            .IsUnique();

        builder.Entity<BasicInformation>()
            .HasOne(b => b.Address)
            .WithOne()
            .HasForeignKey<BasicInformation>(b => b.AddressId);
        builder.Entity<BasicInformation>()
            .HasData(new BasicInformation { BasicInformationId = 1, AddressId = 2, Name = "Filip", Surname = "Juroszek", Email = "filip@wp.pl", Phone = "345678904" });
    }
}