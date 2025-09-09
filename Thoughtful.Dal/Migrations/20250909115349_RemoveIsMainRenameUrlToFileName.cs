using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thoughtful.Dal.Migrations
{
    public partial class RemoveIsMainRenameUrlToFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProfilePhotos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ProfilePhotos",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ProfilePhotos",
                newName: "Url");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProfilePhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
