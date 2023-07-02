using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFish.UI.Migrations
{
    public partial class CategoriesAddedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowInHomePage",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ShowInHomePage",
                table: "Categories");
        }
    }
}
