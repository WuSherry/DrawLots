using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawLots.Migrations
{
    public partial class lotsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    獎項 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    人數 = table.Column<int>(type: "int", nullable: false),
                    得獎者 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lots");
        }
    }
}
