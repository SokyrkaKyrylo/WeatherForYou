using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForYou.Domain.Migrations
{
    public partial class AddTemperatureToDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Temperature",
                table: "Meteorologies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Meteorologies");
        }
    }
}
