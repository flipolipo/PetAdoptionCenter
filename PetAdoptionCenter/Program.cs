using SimpleWebDal.Data;
using SimpleWebDal.Repository.ShelterRepo;
using SimpleWebDal.Repository.UserRepo;
using SImpleWebLogic.Configuration;
using SImpleWebLogic.Extensions;
using SImpleWebLogic.Repository.ShelterRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationDependencies();
builder.Services.ConfigureAutoMapper();
builder.Services.AddDbContext<PetAdoptionCenterContext>();
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ValidatorFactory>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
