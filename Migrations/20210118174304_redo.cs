using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFish.Migrations
{
    public partial class redo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    InStock = table.Column<bool>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    ShoppingCartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeatured", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "https://i.imgur.com/jDlwQfT.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" },
                    { 2, "https://i.imgur.com/jM45dWU.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "ჩაიდანი", 14m, "ჩაიდანი მოკლედ რაღაც ინფორმაცია" },
                    { 3, "https://i.imgur.com/AIwoQYN.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "ანკესი", 16m, "ანკესი მოკლედ" },
                    { 4, "https://i.imgur.com/jDlwQfT.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" },
                    { 5, "https://i.imgur.com/jM45dWU.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "ჩაიდანი", 14m, "ჩაიდანი მოკლედ რაღაც ინფორმაცია" },
                    { 6, "https://i.imgur.com/AIwoQYN.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "ანკესი", 16m, "ანკესი მოკლედ" },
                    { 7, "https://i.imgur.com/jDlwQfT.png", null, true, true, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ", "მანქანის გადასაფარებელი", 12m, "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ID", "ImageURL", "ProductID" },
                values: new object[,]
                {
                    { 1, "https://i.imgur.com/APSUSkV.jpg", 1 },
                    { 2, "https://i.imgur.com/FSTeMK2.jpg", 1 },
                    { 3, "https://i.imgur.com/qKxxqUb.jpg", 1 },
                    { 4, "https://i.imgur.com/QfIRm5K.jpg", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductID",
                table: "Images",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
