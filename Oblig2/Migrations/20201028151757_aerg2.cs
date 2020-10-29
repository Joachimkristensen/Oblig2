using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class aerg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUser_AspNetUsers_ApplicationUserId",
                table: "BlogApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUser_Blogs_BlogId",
                table: "BlogApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUser_AspNetUsers_UserId1",
                table: "BlogApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogApplicationUser",
                table: "BlogApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplicationUser_ApplicationUserId",
                table: "BlogApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplicationUser_UserId1",
                table: "BlogApplicationUser");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BlogApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BlogApplicationUser");

            migrationBuilder.RenameTable(
                name: "BlogApplicationUser",
                newName: "BlogApplicationUsers");

            migrationBuilder.RenameIndex(
                name: "IX_BlogApplicationUser_BlogId",
                table: "BlogApplicationUsers",
                newName: "IX_BlogApplicationUsers_BlogId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BlogApplicationUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogApplicationUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUsers_UserId",
                table: "BlogApplicationUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUsers_Blogs_BlogId",
                table: "BlogApplicationUsers",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUsers_AspNetUsers_UserId",
                table: "BlogApplicationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUsers_Blogs_BlogId",
                table: "BlogApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplicationUsers_AspNetUsers_UserId",
                table: "BlogApplicationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogApplicationUsers",
                table: "BlogApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplicationUsers_UserId",
                table: "BlogApplicationUsers");

            migrationBuilder.RenameTable(
                name: "BlogApplicationUsers",
                newName: "BlogApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_BlogApplicationUsers_BlogId",
                table: "BlogApplicationUser",
                newName: "IX_BlogApplicationUser_BlogId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BlogApplicationUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogApplicationUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BlogApplicationUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BlogApplicationUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogApplicationUser",
                table: "BlogApplicationUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_ApplicationUserId",
                table: "BlogApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_UserId1",
                table: "BlogApplicationUser",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUser_AspNetUsers_ApplicationUserId",
                table: "BlogApplicationUser",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUser_Blogs_BlogId",
                table: "BlogApplicationUser",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplicationUser_AspNetUsers_UserId1",
                table: "BlogApplicationUser",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
