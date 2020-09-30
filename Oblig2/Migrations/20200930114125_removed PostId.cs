using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class removedPostId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Post_PostId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_PostId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogId1",
                table: "Post",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId1",
                table: "Post",
                column: "BlogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_BlogId1",
                table: "Post",
                column: "BlogId1",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_BlogId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BlogId1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BlogId1",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PostId",
                table: "Blogs",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Post_PostId",
                table: "Blogs",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
