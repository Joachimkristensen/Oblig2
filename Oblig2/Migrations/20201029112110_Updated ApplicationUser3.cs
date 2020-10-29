using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class UpdatedApplicationUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUsers_AspNetUsers_ApplicationUserId",
                table: "BlogApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplicationUsers_ApplicationUserId",
                table: "BlogApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BlogApplicationUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BlogApplicationUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUsers_ApplicationUserId",
                table: "BlogApplicationUsers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUsers_AspNetUsers_ApplicationUserId",
                table: "BlogApplicationUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
