using HotelAbshar.Domain.Entities.Discount;
using HotelAbshar.Domain.Entities.Hotels;
using HotelAbshar.Domain.Entities.News;
using HotelAbshar.Domain.Entities.Order_Cart;
using HotelAbshar.Domain.Entities.Permission;
using HotelAbshar.Domain.Entities.Product;
using HotelAbshar.Domain.Entities.Reservation;
using HotelAbshar.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelAbshar.Application.Interfaces
{
   public interface IAbsharContext
    {
        DbSet<Wallet> Wallets { get; set; }
        DbSet<WalletType> WalletTypes { get; set; }   DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<Hotel> Hotels { get; set; }
        DbSet<HotelImages> HotelImages { get; set; }
        DbSet<HotelFeatures> HotelFeatures { get; set; }
        DbSet<HotelCity> HotelCities { get; set; }
        DbSet<HotelProvince> HotelProvinces { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<HotelRoom> HotelRooms { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<travelerReservation> TravelerReservations { get; set; }
        DbSet<UserHotel> UserHotels { get; set; }
        DbSet<News> News { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductFeature> ProductFeatures { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ProductDiscount> ProductDiscounts { get; set; }
        DbSet<HotelDiscount> HotelDiscounts { get; set; }
        DbSet<UserProductDiscount> UserProductDiscounts { get; set; }
        DbSet<UserHotelDiscount> UserHotelDiscounts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<HotelComment> HotelComments { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
