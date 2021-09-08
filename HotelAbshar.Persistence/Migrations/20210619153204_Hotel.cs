using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class Hotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelProvinces",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelProvinces", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "HotelCities",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    HotelProvinceProvinceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelCities", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_HotelCities_HotelProvinces_HotelProvinceProvinceID",
                        column: x => x.HotelProvinceProvinceID,
                        principalTable: "HotelProvinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    StarCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorCount = table.Column<int>(type: "int", nullable: false),
                    HotelAddress = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    HotelCityCityID = table.Column<int>(type: "int", nullable: true),
                    HotelProvinceProvinceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_Hotels_HotelCities_HotelCityCityID",
                        column: x => x.HotelCityCityID,
                        principalTable: "HotelCities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hotels_HotelProvinces_HotelProvinceProvinceID",
                        column: x => x.HotelProvinceProvinceID,
                        principalTable: "HotelProvinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelFeatures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureTitle = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFeatures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HotelFeatures_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelCities_HotelProvinceProvinceID",
                table: "HotelCities",
                column: "HotelProvinceProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFeatures_HotelID",
                table: "HotelFeatures",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelID",
                table: "HotelImages",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelCityCityID",
                table: "Hotels",
                column: "HotelCityCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelProvinceProvinceID",
                table: "Hotels",
                column: "HotelProvinceProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelFeatures");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelCities");

            migrationBuilder.DropTable(
                name: "HotelProvinces");
        }
    }
}
