using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebDal.Migrations
{
    /// <inheritdoc />
    public partial class _4city : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "FlatNumber", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { 4L, "Gdynia", 3, "3a", "48-456", "Janasa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
