using Microsoft.EntityFrameworkCore.Migrations;

namespace Satellites.Persistence.Migrations
{
    public partial class defaultvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 1,
                column: "Position",
                value: "-500,-200");

            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 2,
                column: "Position",
                value: "100,-100");

            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 3,
                column: "Position",
                value: "500,100");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 1,
                column: "Position",
                value: "-500, -200");

            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 2,
                column: "Position",
                value: "100, -100");

            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 3,
                column: "Position",
                value: "500, 100");
        }
    }
}
