using Microsoft.EntityFrameworkCore.Migrations;

namespace Satellites.Persistence.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<float>(type: "real", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 1, null, "", "", "-500, -200" });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 2, null, "", "", "-500, -200" });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "Id", "Distance", "Message", "Name", "Position" },
                values: new object[] { 3, null, "", "", "-500, -200" });

            migrationBuilder.CreateIndex(
                name: "IX_Satellites_Id",
                table: "Satellites",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
