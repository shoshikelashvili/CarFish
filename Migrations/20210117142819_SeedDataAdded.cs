using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFish.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 1, "https://i.imgur.com/jDlwQfT.png", null, false, true, null, "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 2, "https://i.imgur.com/jM45dWU.png", null, false, true, null, "ჩაიდანი", 14m, "ჩაიდანი მოკლედ რაღაც ინფორმაცია" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 3, "https://i.imgur.com/AIwoQYN.png", null, false, true, null, "ანკესი", 16m, "ანკესი მოკლედ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);
        }
    }
}
