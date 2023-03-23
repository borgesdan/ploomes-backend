using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ploomes.API.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTitle",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTitle",
                table: "Orders");
        }
    }
}
