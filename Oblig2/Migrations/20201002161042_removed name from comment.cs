using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class removednamefromcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
