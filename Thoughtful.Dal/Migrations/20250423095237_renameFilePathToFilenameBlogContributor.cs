using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thoughtful.Dal.Migrations
{
    public partial class renameFilePathToFilenameBlogContributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "BlogContributor",
                newName: "Filename");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "BlogContributor",
                newName: "FilePath");
        }
    }
}
