using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class AddedmemberstoBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationDate",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Blogs");
        }
    }
}
