using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrawLots.Migrations
{
    public partial class Activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Item",
                table: "Histories",
                newName: "獎項");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "獎項",
                table: "Histories",
                newName: "Item");
        }
    }
}
