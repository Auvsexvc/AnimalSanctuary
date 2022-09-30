using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalSanctuaryAPI.Migrations
{
    public partial class add_simple_image_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Images", x => x.Id));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}