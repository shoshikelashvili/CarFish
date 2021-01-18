using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFish.Migrations
{
    public partial class _7prdcts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 7, "https://i.imgur.com/jDlwQfT.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);
        }
    }
}
