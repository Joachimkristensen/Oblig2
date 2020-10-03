using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class Addedmemberstopost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_BlogId1",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_Post_OwnerId",
                table: "Posts",
                newName: "IX_Posts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_BlogId1",
                table: "Posts",
                newName: "IX_Posts_BlogId1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_PostId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId1",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_OwnerId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_OwnerId",
                table: "Post",
                newName: "IX_Post_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BlogId1",
                table: "Post",
                newName: "IX_Post_BlogId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId1",
                table: "Comment",
                column: "PostId1",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_BlogId1",
                table: "Post",
                column: "BlogId1",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
