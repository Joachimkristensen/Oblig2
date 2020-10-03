using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class Addedpropertiesincomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comments");
        }
    }
}
