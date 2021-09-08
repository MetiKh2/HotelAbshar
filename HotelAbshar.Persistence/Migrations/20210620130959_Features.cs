using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class Features : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureTitle",
                table: "HotelFeatures");

            migrationBuilder.AddColumn<int>(
                name: "FeatureID",
                table: "HotelFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureTitle = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelFeatures_FeatureID",
                table: "HotelFeatures",
                column: "FeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelFeatures_Features_FeatureID",
                table: "HotelFeatures",
                column: "FeatureID",
                principalTable: "Features",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelFeatures_Features_FeatureID",
                table: "HotelFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropIndex(
                name: "IX_HotelFeatures_FeatureID",
                table: "HotelFeatures");

            migrationBuilder.DropColumn(
                name: "FeatureID",
                table: "HotelFeatures");

            migrationBuilder.AddColumn<string>(
                name: "FeatureTitle",
                table: "HotelFeatures",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");
        }
    }
}
