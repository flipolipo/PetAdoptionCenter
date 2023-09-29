using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleWebDal.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
    public partial class test : Migration
========
    public partial class addRoles : Migration
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    FlatNumber = table.Column<int>(type: "integer", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicHealthInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicHealthInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarActivities",
                columns: table => new
                {
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
========
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateWithTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
========
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
                });

            migrationBuilder.CreateTable(
                name: "BasicInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInformations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameOfdisease = table.Column<string>(type: "text", nullable: false),
                    IllnessStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IllnessEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BasicHealthInfoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccinationName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BasicHealthInfoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                    Name = table.Column<string>(type: "text", nullable: false),
                    ActivityDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CalendarActivityId = table.Column<Guid>(type: "uuid", nullable: false)
========
                    Name = table.Column<string>(type: "text", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CalendarActivityId = table.Column<Guid>(type: "uuid", nullable: true)
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_CalendarActivities_CalendarActivityId",
                        column: x => x.CalendarActivityId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShelterDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shelters_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shelters_CalendarActivities_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CredentialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasicInformationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserCalendarId = table.Column<Guid>(type: "uuid", nullable: false),
========
                name: "Adoptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfAdoption = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adoptions_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BasicInformationId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserCalendarId = table.Column<Guid>(type: "uuid", nullable: true),
                    PetId = table.Column<Guid>(type: "uuid", nullable: true),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_BasicInformations_BasicInformationId",
                        column: x => x.BasicInformationId,
                        principalTable: "BasicInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CalendarActivities_UserCalendarId",
                        column: x => x.UserCalendarId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempHouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartOfTemporaryHouseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BasicInformations_BasicInformationId",
                        column: x => x.BasicInformationId,
                        principalTable: "BasicInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_CalendarActivities_UserCalendarId",
                        column: x => x.UserCalendarId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Adoptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreAdoptionPoll = table.Column<bool>(type: "boolean", nullable: false),
                    ContractAdoption = table.Column<bool>(type: "boolean", nullable: false),
                    Meetings = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfAdoption = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adoptions_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Adoptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TempHouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartOfTemporaryHouseDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempHouses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHouses_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TempHouses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pets = table.Column<List<string>>(type: "text[]", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
========
                    table.PrimaryKey("PK_TempHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempHouses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHouses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHouses_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    BasicHealthInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AvaibleForAdoption = table.Column<bool>(type: "boolean", nullable: false),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: true),
                    TempHouseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_CalendarActivities_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_TempHouses_TempHouseId",
                        column: x => x.TempHouseId,
                        principalTable: "TempHouses",
                        principalColumn: "Id");
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    BasicHealthInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AvaibleForAdoption = table.Column<bool>(type: "boolean", nullable: false),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TempHouseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_CalendarActivities_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "CalendarActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_TempHouses_TempHouseId",
                        column: x => x.TempHouseId,
                        principalTable: "TempHouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatronsUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Patrons = table.Column<List<string>>(type: "text[]", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatronsUsers_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CalendarActivityId",
                table: "Activities",
                column: "CalendarActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_ShelterId",
                table: "Adoptions",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BasicInformationId",
                table: "AspNetUsers",
                column: "BasicInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PetId",
                table: "AspNetUsers",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCalendarId",
                table: "AspNetUsers",
                column: "UserCalendarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformations_AddressId",
                table: "BasicInformations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_BasicHealthInfoId",
                table: "Diseases",
                column: "BasicHealthInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PatronsUsers_PetId",
                table: "PatronsUsers",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BasicHealthInfoId",
                table: "Pets",
                column: "BasicHealthInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CalendarId",
                table: "Pets",
                column: "CalendarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ShelterId",
                table: "Pets",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TempHouseId",
                table: "Pets",
                column: "TempHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_AddressId",
                table: "Shelters",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_CalendarId",
                table: "Shelters",
                column: "CalendarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempHouses_AddressId",
                table: "TempHouses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHouses_ShelterId",
                table: "TempHouses",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHouses_UserId",
                table: "TempHouses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPets_UserId",
                table: "UserPets",
                column: "UserId");

            migrationBuilder.CreateIndex(
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                name: "IX_Users_BasicInformationId",
                table: "Users",
                column: "BasicInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialsId",
                table: "Users",
                column: "CredentialsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShelterId",
                table: "Users",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCalendarId",
                table: "Users",
                column: "UserCalendarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_BasicHealthInfoId",
                table: "Vaccinations",
                column: "BasicHealthInfoId");
========
                name: "IX_Vaccinations_BasicHealthInfoId",
                table: "Vaccinations",
                column: "BasicHealthInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_AspNetUsers_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Pets_PetId",
                table: "Adoptions",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pets_PetId",
                table: "AspNetUsers",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
========
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CalendarActivities_UserCalendarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_CalendarActivities_CalendarId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelters_CalendarActivities_CalendarId",
                table: "Shelters");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHouses_AspNetUsers_UserId",
                table: "TempHouses");

>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Adoptions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
<<<<<<<< HEAD:SimpleWebDal/Migrations/20230928114508_test.cs
                name: "PatronsUsers");
========
                name: "ProfilePets");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Profiles");
>>>>>>>> Roles:SimpleWebDal/Migrations/20230929115147_addRoles.cs

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserPets");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BasicInformations");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "BasicHealthInfos");

            migrationBuilder.DropTable(
                name: "TempHouses");

            migrationBuilder.DropTable(
                name: "Shelters");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CalendarActivities");
        }
    }
}
