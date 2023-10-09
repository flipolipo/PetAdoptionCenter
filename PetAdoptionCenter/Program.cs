using SimpleWebDal.Data;
using SimpleWebDal.Repository.ShelterRepo;
using SimpleWebDal.Repository.UserRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Configuration;
using SImpleWebLogic.Extensions;
using System.Text;
using SImpleWebLogic.Repository.ShelterRepo;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

AddServices();
ConfigureSwagger();
AddDbContext(builder.Configuration);
AddAuthentication();
AddIdentity();

builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterApplicationDependencies();
builder.Services.ConfigureAutoMapper();
builder.Services.AddDbContext<PetAdoptionCenterContext>();
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ValidatorFactory>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await AddRoles();
await AddAdmin();

app.Run();

void AddServices()
{

    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();

};
void ConfigureSwagger()
{

    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Adoption Center", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });

};
void AddDbContext(ConfigurationManager dbConfig)
{
    builder.Services.AddDbContext<PetAdoptionCenterContext>(options => options.UseNpgsql(dbConfig.GetConnectionString("MyConnection")));
   

};
void AddIdentity()
{
    builder.Services
        .AddIdentityCore<User>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole<Guid>>() 
        .AddEntityFrameworkStores<PetAdoptionCenterContext>();
}
void AddAuthentication()
{
    var jwtValidation = builder.Configuration.GetValue<string>("IssuerSigningKey");
    var audience = builder.Configuration.GetValue<string>("ValidAudience");
    var issuer = builder.Configuration.GetValue<string>("ValidIssuer");
    builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               ClockSkew = TimeSpan.Zero,
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer =issuer, 
               ValidAudience = audience, 
               IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(jwtValidation)
               ),
           };
       });

};
async Task AddRoles()
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    await CreateAdminRole(roleManager);


    await CreateUserRole(roleManager);


    await CreateShelterAdminRole(roleManager);

}
async Task CreateAdminRole(RoleManager<IdentityRole<Guid>> roleManager)
{
    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin")); //The role string should better be stored as a constant or a value in appsettings
}
async Task CreateUserRole(RoleManager<IdentityRole<Guid>> roleManager)
{
    await roleManager.CreateAsync(new IdentityRole<Guid>("User")); //The role string should better be stored as a constant or a value in appsettings
}
async Task CreateShelterAdminRole(RoleManager<IdentityRole<Guid>> roleManager)
{
    await roleManager.CreateAsync(new IdentityRole<Guid>("ShelterAdmin")); //The role string should better be stored as a constant or a value in appsettings
}
async Task AddAdmin()
{
    await CreateAdminIfNotExists();
}
async Task CreateAdminIfNotExists()
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
    if (adminInDb == null)
    {
        var admin = new User { UserName = "admin", Email = "admin@admin.com" };
        var adminCreated = await userManager.CreateAsync(admin, builder.Configuration.GetValue<string>("AdminPassword"));

        if (adminCreated.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}

