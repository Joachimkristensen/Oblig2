using Microsoft.EntityFrameworkCore.Migrations;

namespace Oblig2.Migrations
{
    public partial class aerg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BlogApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogApplicationUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogApplicationUser_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogApplicationUser_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_ApplicationUserId",
                table: "BlogApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_BlogId",
                table: "BlogApplicationUser",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_UserId1",
                table: "BlogApplicationUser",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogUsers",
                columns: table => new
                {
                    BlogUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
    }
}
