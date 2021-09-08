using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAbshar.Persistence.Migrations
{
    public partial class IsRemovedRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Users_UserID",
                table: "HotelRooms");

            migrationBuilder.DropIndex(
                name: "IX_HotelRooms_UserID",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "EndBooking",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "StartBooking",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "HotelRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "HotelRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "HotelRooms");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndBooking",
                table: "HotelRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartBooking",
                table: "HotelRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "HotelRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_UserID",
                table: "HotelRooms",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Users_UserID",
                table: "HotelRooms",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
