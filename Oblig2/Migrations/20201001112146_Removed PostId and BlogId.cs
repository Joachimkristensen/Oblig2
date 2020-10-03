using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class RemovedPostIdandBlogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_PostId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comment_PostId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "BlogId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comment_PostId",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "BlogId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogId1",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId1",
                table: "Posts",
                column: "BlogId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId1",
                table: "Comment",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_PostId1",
                table: "Comment",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId1",
                table: "Posts",
                column: "BlogId1",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
