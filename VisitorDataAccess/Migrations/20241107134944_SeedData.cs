using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitorDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "On Core" },
                    { 2L, "Integration Team" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "Angelo Dejaeghere" },
                    { 2L, 1L, "Niels Stubbe" },
                    { 3L, 1L, "Mathias Schaemelhout" },
                    { 4L, 2L, "Nathan Moerman" },
                    { 5L, 2L, "Robert Maes" },
                    { 6L, 2L, "Nils Van Butsel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
