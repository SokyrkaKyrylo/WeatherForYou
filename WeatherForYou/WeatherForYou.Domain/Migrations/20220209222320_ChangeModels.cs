using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForYou.Domain.Migrations
{
    public partial class ChangeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meteorologies_Days_DayId",
                table: "Meteorologies");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Meteorologies_DayId",
                table: "Meteorologies");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Meteorologies");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Meteorologies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meteorologies_CityId",
                table: "Meteorologies",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meteorologies_Cities_CityId",
                table: "Meteorologies",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meteorologies_Cities_CityId",
                table: "Meteorologies");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Meteorologies_CityId",
                table: "Meteorologies");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Meteorologies");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Meteorologies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meteorologies_DayId",
                table: "Meteorologies",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meteorologies_Days_DayId",
                table: "Meteorologies",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");
        }
    }
}
