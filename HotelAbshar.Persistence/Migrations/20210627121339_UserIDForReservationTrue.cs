using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class UserIDForReservationTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserID",
                table: "Reservations",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reservations");

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
    }
}
