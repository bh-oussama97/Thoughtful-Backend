using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thoughtful.Dal.Migrations
{
    public partial class addExtensionToBlogContributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "BlogContributor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "BlogContributor");
        }
    }
}
