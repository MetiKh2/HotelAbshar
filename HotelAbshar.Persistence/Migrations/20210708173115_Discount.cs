using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class Discount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelDiscounts",
                columns: table => new
                {
                    HotelDiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsableCount = table.Column<int>(type: "int", nullable: true),
                    DiscountFor = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDiscounts", x => x.HotelDiscountID);
                    table.ForeignKey(
                        name: "FK_HotelDiscounts_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    ProductDiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsableCount = table.Column<int>(type: "int", nullable: true),
                    DiscountFor = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscounts", x => x.ProductDiscountID);
                    table.ForeignKey(
                        name: "FK_ProductDiscounts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserHotelDiscounts",
                columns: table => new
                {
                    UHD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    HotelDiscountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHotelDiscounts", x => x.UHD_ID);
                    table.ForeignKey(
                        name: "FK_UserHotelDiscounts_HotelDiscounts_HotelDiscountID",
                        column: x => x.HotelDiscountID,
                        principalTable: "HotelDiscounts",
                        principalColumn: "HotelDiscountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserHotelDiscounts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProductDiscounts",
                columns: table => new
                {
                    UPD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductDiscountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductDiscounts", x => x.UPD_ID);
                    table.ForeignKey(
                        name: "FK_UserProductDiscounts_ProductDiscounts_ProductDiscountID",
                        column: x => x.ProductDiscountID,
                        principalTable: "ProductDiscounts",
                        principalColumn: "ProductDiscountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductDiscounts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelDiscounts_HotelID",
                table: "HotelDiscounts",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProductID",
                table: "ProductDiscounts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHotelDiscounts_HotelDiscountID",
                table: "UserHotelDiscounts",
                column: "HotelDiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_UserHotelDiscounts_UserID",
                table: "UserHotelDiscounts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductDiscounts_ProductDiscountID",
                table: "UserProductDiscounts",
                column: "ProductDiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductDiscounts_UserID",
                table: "UserProductDiscounts",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserHotelDiscounts");

            migrationBuilder.DropTable(
                name: "UserProductDiscounts");

            migrationBuilder.DropTable(
                name: "HotelDiscounts");

            migrationBuilder.DropTable(
                name: "ProductDiscounts");
        }
    }
}
