using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class ProductGroupes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryID1",
                table: "Products",
                newName: "SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryID1",
                table: "Products",
                newName: "IX_Products_SubCategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Invertory",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ParentCategoryID",
                table: "Products",
                column: "ParentCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_ParentCategoryID",
                table: "Products",
                column: "ParentCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_SubCategoryID",
                table: "Products",
                column: "SubCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_ParentCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_SubCategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ParentCategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentCategoryID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "Products",
                newName: "CategoryID1");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategoryID",
                table: "Products",
                newName: "IX_Products_CategoryID1");

            migrationBuilder.AlterColumn<long>(
                name: "ViewCount",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Invertory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoryID",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID1",
                table: "Products",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
