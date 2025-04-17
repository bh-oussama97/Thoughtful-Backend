using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thoughtful.Dal.Migrations
{
    public partial class BlogAuthorRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Blogs_BlogId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BlogId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "BlogContributors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogContributors", x => new { x.AuthorId, x.BlogId });
                    table.ForeignKey(
                        name: "FK_BlogContributors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogContributors_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogContributors_BlogId",
                table: "BlogContributors",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogContributors");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BlogId",
                table: "Authors",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Blogs_BlogId",
                table: "Authors",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
