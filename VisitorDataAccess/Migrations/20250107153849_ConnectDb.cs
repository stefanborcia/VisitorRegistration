using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConnectDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorLogs_Visits_VisitId",
                table: "VisitorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Visitors_VisitorId",
                table: "Visits");

            migrationBuilder.AlterColumn<long>(
                name: "VisitorId",
                table: "Visits",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "VisitingCompanyId",
                table: "Visits",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentWithId",
                table: "Visits",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "VisitId",
                table: "VisitorLogs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorLogs_Visits_VisitId",
                table: "VisitorLogs",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Visitors_VisitorId",
                table: "Visits",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorLogs_Visits_VisitId",
                table: "VisitorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Visitors_VisitorId",
                table: "Visits");

            migrationBuilder.AlterColumn<long>(
                name: "VisitorId",
                table: "Visits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "VisitingCompanyId",
                table: "Visits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentWithId",
                table: "Visits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "VisitId",
                table: "VisitorLogs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorLogs_Visits_VisitId",
                table: "VisitorLogs",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Visitors_VisitorId",
                table: "Visits",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
