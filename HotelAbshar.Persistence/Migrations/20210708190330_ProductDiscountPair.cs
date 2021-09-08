using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class ProductDiscountPair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscounts_Products_ProductID",
                table: "ProductDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscounts_ProductID",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ProductDiscounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ProductDiscounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProductID",
                table: "ProductDiscounts",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscounts_Products_ProductID",
                table: "ProductDiscounts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
