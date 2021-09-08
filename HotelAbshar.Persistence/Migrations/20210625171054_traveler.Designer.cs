﻿// <auto-generated />
using System;
using HotelAbshar.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelAbshar.Persistence.Migrations
{
    [DbContext(typeof(AbsharContext))]
    [Migration("20210625171054_traveler")]
    partial class traveler
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.Feature", b =>
                {
                    b.Property<int>("FeatureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FeatureTitle")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.HasKey("FeatureID");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.Hotel", b =>
                {
                    b.Property<int>("HotelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FloorsCount")
                        .HasColumnType("int");

                    b.Property<string>("HotelAddress")
                        .IsRequired()
                        .HasMaxLength(900)
                        .HasColumnType("nvarchar(900)");

                    b.Property<int?>("HotelCityCityID")
                        .HasColumnType("int");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("HotelProvinceProvinceID")
                        .HasColumnType("int");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("MinPriceForOneNight")
                        .HasColumnType("int");

                    b.Property<int>("ProvinceID")
                        .HasColumnType("int");

                    b.Property<int?>("StarCount")
                        .HasColumnType("int");

                    b.HasKey("HotelID");

                    b.HasIndex("HotelCityCityID");

                    b.HasIndex("HotelProvinceProvinceID");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelCity", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("HotelProvinceProvinceID")
                        .HasColumnType("int");

                    b.Property<int>("ProvinceID")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.HasIndex("HotelProvinceProvinceID");

                    b.ToTable("HotelCities");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelFeatures", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureID")
                        .HasColumnType("int");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FeatureID");

                    b.HasIndex("HotelID");

                    b.ToTable("HotelFeatures");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelImages", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.HasIndex("HotelID");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelProvince", b =>
                {
                    b.Property<int>("ProvinceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProvinceTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ProvinceID");

                    b.ToTable("HotelProvinces");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelRoom", b =>
                {
                    b.Property<int>("HotelRoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(175)
                        .HasColumnType("nvarchar(175)");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("HotelRoomTitle")
                        .IsRequired()
                        .HasMaxLength(65)
                        .HasColumnType("nvarchar(65)");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("PriceForGuestExtra")
                        .HasColumnType("int");

                    b.Property<int>("PriceForOneNight")
                        .HasColumnType("int");

                    b.HasKey("HotelRoomID");

                    b.HasIndex("HotelID");

                    b.ToTable("HotelRooms");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PermissionID");

                    b.HasIndex("ParentID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Permission.RolePermission", b =>
                {
                    b.Property<int>("RP_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("RP_ID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("RoleID");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<int>("HotelRoomID")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinally")
                        .HasColumnType("bit");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReservationID");

                    b.HasIndex("HotelID");

                    b.HasIndex("HotelRoomID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Reservation.travelerReservation", b =>
                {
                    b.Property<int>("UserReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("ReservationID")
                        .HasColumnType("int");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserReservationID");

                    b.HasIndex("ReservationID");

                    b.HasIndex("UserID");

                    b.ToTable("TravelerReservations");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivationCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemovedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.UserInRole", b =>
                {
                    b.Property<int>("UR_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UR_ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserInRoles");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.Wallet", b =>
                {
                    b.Property<int>("WalletID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("WalletTypeID")
                        .HasColumnType("int");

                    b.HasKey("WalletID");

                    b.HasIndex("UserID");

                    b.HasIndex("WalletTypeID");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.WalletType", b =>
                {
                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.Property<string>("TypeTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("TypeID");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.Hotel", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.HotelCity", "HotelCity")
                        .WithMany()
                        .HasForeignKey("HotelCityCityID");

                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.HotelProvince", "HotelProvince")
                        .WithMany()
                        .HasForeignKey("HotelProvinceProvinceID");

                    b.Navigation("HotelCity");

                    b.Navigation("HotelProvince");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelCity", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.HotelProvince", "HotelProvince")
                        .WithMany("HotelCities")
                        .HasForeignKey("HotelProvinceProvinceID");

                    b.Navigation("HotelProvince");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelFeatures", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.Feature", "Feature")
                        .WithMany("HotelFeatures")
                        .HasForeignKey("FeatureID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("HotelFeatures")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelImages", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("HotelImages")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelRoom", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("HotelRooms")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Permission.Permission", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Permission.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Permission.RolePermission", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Permission.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.User.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.Hotel", "Hotel")
                        .WithMany("Reservations")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.Hotels.HotelRoom", "HotelRoom")
                        .WithMany("Reservations")
                        .HasForeignKey("HotelRoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("HotelRoom");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Reservation.travelerReservation", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.Reservation.Reservation", "Reservation")
                        .WithMany("TravelerReservations")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.UserInRole", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.User.Role", "Role")
                        .WithMany("UserInRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.User.User", "User")
                        .WithMany("UserInRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.Wallet", b =>
                {
                    b.HasOne("HotelAbshar.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelAbshar.Domain.Entities.User.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("WalletTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.Feature", b =>
                {
                    b.Navigation("HotelFeatures");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.Hotel", b =>
                {
                    b.Navigation("HotelFeatures");

                    b.Navigation("HotelImages");

                    b.Navigation("HotelRooms");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelProvince", b =>
                {
                    b.Navigation("HotelCities");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Hotels.HotelRoom", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Permission.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Navigation("TravelerReservations");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.Role", b =>
                {
                    b.Navigation("UserInRoles");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.User", b =>
                {
                    b.Navigation("UserInRoles");
                });

            modelBuilder.Entity("HotelAbshar.Domain.Entities.User.WalletType", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
