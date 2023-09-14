using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleWebDal.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdoptionInfos",
                columns: table => new
                {
                    AdoptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfAdoption = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionInfos", x => x.AdoptionId);
                });

            migrationBuilder.CreateTable(
                name: "TempHouses",
                columns: table => new
                {
                    TempHouseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartOfTemporaryHouseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHouses", x => x.TempHouseId);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AcctivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CalendarId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    FlatNumber = table.Column<int>(type: "integer", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    BasicInformationId = table.Column<int>(type: "integer", nullable: false),
                    ShelterId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    TempHouseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_TempHouses_TempHouseId",
                        column: x => x.TempHouseId,
                        principalTable: "TempHouses",
                        principalColumn: "TempHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicHealthInfos",
                columns: table => new
                {
                    BasicHealthInfoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    PetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicHealthInfos", x => x.BasicHealthInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfdisease = table.Column<string>(type: "text", nullable: false),
                    IllnessStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IllnessEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BasicHealthInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.DiseaseId);
                    table.ForeignKey(
                        name: "FK_Diseases_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "BasicHealthInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    VaccinationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VaccinationName = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BasicHealthInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.VaccinationId);
                    table.ForeignKey(
                        name: "FK_Vaccinations_BasicHealthInfos_BasicHealthInfoId",
                        column: x => x.BasicHealthInfoId,
                        principalTable: "BasicHealthInfos",
                        principalColumn: "BasicHealthInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicInformations",
                columns: table => new
                {
                    BasicInformationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformations", x => x.BasicInformationId);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    CalendarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateWithTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShelterId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.CalendarId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AvaibleForAdoption = table.Column<bool>(type: "boolean", nullable: false),
                    AdoptionId = table.Column<int>(type: "integer", nullable: false),
                    TempHouseId = table.Column<int>(type: "integer", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: false),
                    ProfileIdForVirtualAdoptionId = table.Column<int>(type: "integer", nullable: false),
                    ShelterListOfPetsAdoptedId = table.Column<int>(type: "integer", nullable: false),
                    ShelterListOfPetsId = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_AdoptionInfos_AdoptionId",
                        column: x => x.AdoptionId,
                        principalTable: "AdoptionInfos",
                        principalColumn: "AdoptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_TempHouses_TempHouseId",
                        column: x => x.TempHouseId,
                        principalTable: "TempHouses",
                        principalColumn: "TempHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    ShelterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShelterDescription = table.Column<string>(type: "text", nullable: false),
                    AdoptionId = table.Column<int>(type: "integer", nullable: false),
                    TempHouseId = table.Column<int>(type: "integer", nullable: false),
                    PetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelters", x => x.ShelterId);
                    table.ForeignKey(
                        name: "FK_Shelters_AdoptionInfos_AdoptionId",
                        column: x => x.AdoptionId,
                        principalTable: "AdoptionInfos",
                        principalColumn: "AdoptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shelters_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shelters_TempHouses_TempHouseId",
                        column: x => x.TempHouseId,
                        principalTable: "TempHouses",
                        principalColumn: "TempHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ShelterId = table.Column<int>(type: "integer", nullable: false),
                    TempHouseId = table.Column<int>(type: "integer", nullable: false),
                    PetId = table.Column<int>(type: "integer", nullable: false),
                    AdoptionId = table.Column<int>(type: "integer", nullable: false),
                    ShelterWorkersId = table.Column<int>(type: "integer", nullable: false),
                    ShelterContributorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_AdoptionInfos_AdoptionId",
                        column: x => x.AdoptionId,
                        principalTable: "AdoptionInfos",
                        principalColumn: "AdoptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Shelters_ShelterContributorsId",
                        column: x => x.ShelterContributorsId,
                        principalTable: "Shelters",
                        principalColumn: "ShelterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "ShelterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Shelters_ShelterWorkersId",
                        column: x => x.ShelterWorkersId,
                        principalTable: "Shelters",
                        principalColumn: "ShelterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_TempHouses_UserId",
                        column: x => x.UserId,
                        principalTable: "TempHouses",
                        principalColumn: "TempHouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserLoggedUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserLoggedUserId",
                        column: x => x.UserLoggedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityId",
                table: "Activities",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CalendarId",
                table: "Activities",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressId",
                table: "Addresses",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BasicInformationId",
                table: "Addresses",
                column: "BasicInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RoleId",
                table: "Addresses",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ShelterId",
                table: "Addresses",
                column: "ShelterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TempHouseId",
                table: "Addresses",
                column: "TempHouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionInfos_AdoptionId",
                table: "AdoptionInfos",
                column: "AdoptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicHealthInfos_BasicHealthInfoId",
                table: "BasicHealthInfos",
                column: "BasicHealthInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicHealthInfos_PetId",
                table: "BasicHealthInfos",
                column: "PetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformations_BasicInformationId",
                table: "BasicInformations",
                column: "BasicInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformations_UserId",
                table: "BasicInformations",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_CalendarId",
                table: "Calendars",
                column: "CalendarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_PetId",
                table: "Calendars",
                column: "PetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_ShelterId",
                table: "Calendars",
                column: "ShelterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_BasicHealthInfoId",
                table: "Diseases",
                column: "BasicHealthInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_DiseaseId",
                table: "Diseases",
                column: "DiseaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ActivityId",
                table: "Pets",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AdoptionId",
                table: "Pets",
                column: "AdoptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetId",
                table: "Pets",
                column: "PetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ProfileId",
                table: "Pets",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ProfileIdForVirtualAdoptionId",
                table: "Pets",
                column: "ProfileIdForVirtualAdoptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ShelterListOfPetsAdoptedId",
                table: "Pets",
                column: "ShelterListOfPetsAdoptedId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ShelterListOfPetsId",
                table: "Pets",
                column: "ShelterListOfPetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TempHouseId",
                table: "Pets",
                column: "TempHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ProfileId",
                table: "Profiles",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserLoggedUserId",
                table: "Profiles",
                column: "UserLoggedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleId",
                table: "Roles",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_AdoptionId",
                table: "Shelters",
                column: "AdoptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_PetId",
                table: "Shelters",
                column: "PetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_ShelterId",
                table: "Shelters",
                column: "ShelterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_TempHouseId",
                table: "Shelters",
                column: "TempHouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempHouses_TempHouseId",
                table: "TempHouses",
                column: "TempHouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdoptionId",
                table: "Users",
                column: "AdoptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PetId",
                table: "Users",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShelterContributorsId",
                table: "Users",
                column: "ShelterContributorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShelterId",
                table: "Users",
                column: "ShelterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShelterWorkersId",
                table: "Users",
                column: "ShelterWorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_BasicHealthInfoId",
                table: "Vaccinations",
                column: "BasicHealthInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccinationId",
                table: "Vaccinations",
                column: "VaccinationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Calendars_CalendarId",
                table: "Activities",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "CalendarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_BasicInformations_BasicInformationId",
                table: "Addresses",
                column: "BasicInformationId",
                principalTable: "BasicInformations",
                principalColumn: "BasicInformationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Roles_RoleId",
                table: "Addresses",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Shelters_ShelterId",
                table: "Addresses",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasicHealthInfos_Pets_PetId",
                table: "BasicHealthInfos",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasicInformations_Users_UserId",
                table: "BasicInformations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Pets_PetId",
                table: "Calendars",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Shelters_ShelterId",
                table: "Calendars",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Profiles_ProfileId",
                table: "Pets",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Profiles_ProfileIdForVirtualAdoptionId",
                table: "Pets",
                column: "ProfileIdForVirtualAdoptionId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Shelters_ShelterListOfPetsAdoptedId",
                table: "Pets",
                column: "ShelterListOfPetsAdoptedId",
                principalTable: "Shelters",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Shelters_ShelterListOfPetsId",
                table: "Pets",
                column: "ShelterListOfPetsId",
                principalTable: "Shelters",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Calendars_CalendarId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Shelters_ShelterListOfPetsAdoptedId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Shelters_ShelterListOfPetsId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shelters_ShelterContributorsId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shelters_ShelterId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shelters_ShelterWorkersId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_TempHouses_TempHouseId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_TempHouses_UserId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pets_PetId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "BasicInformations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BasicHealthInfos");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Shelters");

            migrationBuilder.DropTable(
                name: "TempHouses");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AdoptionInfos");
        }
    }
}
