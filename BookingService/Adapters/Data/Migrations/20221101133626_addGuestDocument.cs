using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addGuestDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentId_DocumentType",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId_IdNumber",
                table: "Guests",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId_DocumentType",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "DocumentId_IdNumber",
                table: "Guests");
        }
    }
}
