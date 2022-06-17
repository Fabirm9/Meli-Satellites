using Microsoft.EntityFrameworkCore.Migrations;

namespace Satellites.Persistence.Migrations
{
    public partial class deletedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "Id",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 1, null, "", "kenobi", "-500,-200" });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 2, null, "", "skywalker", "100,-100" });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 3, null, "", "sato", "500,100" });
        }
    }
}
