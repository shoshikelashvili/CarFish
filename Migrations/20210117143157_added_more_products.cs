using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFish.Migrations
{
    public partial class added_more_products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 4, "https://i.imgur.com/jDlwQfT.png", null, false, true, null, "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 5, "https://i.imgur.com/jM45dWU.png", null, false, true, null, "ჩაიდანი", 14m, "ჩაიდანი მოკლედ რაღაც ინფორმაცია" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[] { 6, "https://i.imgur.com/AIwoQYN.png", null, false, true, null, "ანკესი", 16m, "ანკესი მოკლედ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);
        }
    }
}
