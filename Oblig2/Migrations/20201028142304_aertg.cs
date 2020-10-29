using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class aertg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlogUsers",
                columns: table => new
                {
                    BlogUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogUsers", x => x.BlogUserId);
                    table.ForeignKey(
                        name: "FK_BlogUsers_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ApplicationUserId",
                table: "Blogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogUsers_BlogId",
                table: "BlogUsers",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogUsers_UserId",
                table: "BlogUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_ApplicationUserId",
                table: "Blogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_ApplicationUserId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogUsers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ApplicationUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
