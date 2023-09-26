using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public class UsersContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
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

        options.UseNpgsql(connectionString);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}