using HotelAbshar.Application.Interfaces;
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
using System.Threading.Tasks;

namespace HotelAbshar.Persistence.Context
{
    public class AbsharContext : DbContext, IAbsharContext
    {
        public AbsharContext(DbContextOptions<AbsharContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Wallet>  Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        #endregion
        #region Permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Hotel
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImages> HotelImages { get; set; }
        public DbSet<HotelFeatures> HotelFeatures { get; set; }
        public DbSet<HotelCity> HotelCities { get; set; }
        public DbSet<HotelProvince> HotelProvinces { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<UserHotel> UserHotels { get; set; }
        public DbSet<HotelComment> HotelComments { get; set; }

        #endregion

        #region Reservation
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<travelerReservation> TravelerReservations { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        #endregion

        #region News
        public DbSet<News> News { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Discount
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<HotelDiscount> HotelDiscounts { get; set; }
        public DbSet<UserProductDiscount> UserProductDiscounts { get; set; }
        public DbSet<UserHotelDiscount> UserHotelDiscounts { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Feature>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Hotel>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HotelRoom>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Reservation>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<travelerReservation>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<News>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HotelDiscount>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductDiscount>().HasQueryFilter(p => !p.IsRemoved);

            base.OnModelCreating(modelBuilder);
        }
    }
}
