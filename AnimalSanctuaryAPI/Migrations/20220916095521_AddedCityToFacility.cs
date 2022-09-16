using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalSanctuaryAPI.Migrations
{
    public partial class AddedCityToFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Facilities");
        }
    }
}