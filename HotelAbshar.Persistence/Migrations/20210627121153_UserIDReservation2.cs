using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class UserIDReservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "TravelerReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelerReservations_UserID",
                table: "TravelerReservations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelerReservations_Users_UserID",
                table: "TravelerReservations",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelerReservations_Users_UserID",
                table: "TravelerReservations");

            migrationBuilder.DropIndex(
                name: "IX_TravelerReservations_UserID",
                table: "TravelerReservations");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "TravelerReservations");
        }
    }
}
