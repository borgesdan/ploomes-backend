using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ploomes.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sellerItems_Users_SellerId",
                table: "sellerItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sellerItems",
                table: "sellerItems");

            migrationBuilder.RenameTable(
                name: "sellerItems",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_sellerItems_SellerId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "sellerItems");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "sellerItems",
                newName: "IX_sellerItems_SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sellerItems",
                table: "sellerItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sellerItems_Users_SellerId",
                table: "sellerItems",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
