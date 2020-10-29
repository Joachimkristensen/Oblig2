using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class aerg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlogApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "BlogApplicationUserId",
                table: "BlogApplicationUsers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers",
                column: "BlogApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers");

            migrationBuilder.DropColumn(
                name: "BlogApplicationUserId",
                table: "BlogApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BlogApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers",
                column: "Id");
        }
    }
}
