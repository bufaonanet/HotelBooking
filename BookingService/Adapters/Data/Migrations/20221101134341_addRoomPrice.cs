using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addRoomPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price_Currency",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_Value",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Currency",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Price_Value",
                table: "Rooms");
        }
    }
}
