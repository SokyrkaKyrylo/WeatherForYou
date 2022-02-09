using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForYou.Domain.Migrations
{
    public partial class Addanewpropertytoday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "Days");
        }
    }
}
