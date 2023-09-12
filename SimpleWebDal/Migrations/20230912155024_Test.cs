using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebDal.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_Id",
                table: "Addresses",
                newName: "IX_Addresses_AddressId");

            migrationBuilder.InsertData(
                table: "BasicInformations",
                columns: new[] { "BasicInformationId", "AddressId", "Email", "Name", "Phone", "Surname" },
                values: new object[] { 1, 2, "filip@wp.pl", "Filip", "345678904", "Juroszek" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BasicInformations",
                keyColumn: "BasicInformationId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AddressId",
                table: "Addresses",
                newName: "IX_Addresses_Id");
        }
    }
}
