using Microsoft.EntityFrameworkCore;
using SimpleWebDal.Models.Calendar;
using SimpleWebDal.Models.WebUser;



namespace SimpleWebDal.Data;

public class PetAdoptionCenterContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasicInformation> BasicInformations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TimeTable> TimeTables { get; set; }
    public DbSet<Activity> Activitys { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Wpisz swoja sciezke");
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
        builder.Entity<Role>()
            .HasIndex(u => u.RoleId)
            .IsUnique();

        builder.Entity<Role>()
                  .HasOne(b => b.Address)
                  .WithOne()
                  .HasForeignKey<Role>(b => b.AddressId);

        builder.Entity<User>()
             .HasMany(u => u.Roles)
             .WithOne(r => r.User)
             .HasForeignKey(r => r.UserId);

        builder.Entity<User>()
             .HasOne(b => b.UserTimeTable)
             .WithOne()
             .HasForeignKey<User>(b => b.TimeTableId);
    }
}