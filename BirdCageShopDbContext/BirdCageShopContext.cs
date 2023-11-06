using System;
using System.Collections.Generic;
using BirdCageShopDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BirdCageShopDbContext.Models
{
    public partial class BirdCageShopContext : IdentityDbContext<IdentityUser>
    {


        public BirdCageShopContext(DbContextOptions<BirdCageShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Order { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetail { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Feature> Feature { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<Specification> Specification { get; set; } = null!;
        //public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;
        //public virtual DbSet<WishlistItem> WishlistItems { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<BirdCageType> BirdCageTypes { get; set; } = null!;
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;
        public virtual DbSet<ProductFeature> ProductFeature { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            //

            SeedStatus(modelBuilder);
            SeedRoles(modelBuilder);
            //
            //   modelBuilder.Entity<ApplicationUser>()
            //.HasOne(u => u.W)   // User has one Wishlist
            //.WithOne(w => w.ApplicationUser)      // Wishlist has one User
            //.HasForeignKey<Wishlist>(w => w.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>()
    .HasMany(p => p.Wishlists)
    .WithOne(sc => sc.ApplicationUser)
    .HasForeignKey(sc => sc.ApplicationUserId);

            //modelBuilder.Entity<Order>()
            //    .Property(o => o.TotalPrice)
            //    .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Feature>()
                .Property(pf => pf.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Specification>()
                .Property(ps => ps.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Voucher>()
                .Property(v => v.DiscountPercent)
                .HasColumnType("decimal(18, 2)");

            // Configure the Voucher entity
            modelBuilder.Entity<Voucher>()
                .HasIndex(v => v.VoucherCode)
                .IsUnique();

            modelBuilder.Entity<Product>()
           .HasOne(p => p.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
       .HasMany(p => p.ShoppingCarts)
       .WithOne(sc => sc.Product)
       .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<ApplicationUser>()
    .HasMany(u => u.ShoppingCarts)
    .WithOne(sc => sc.ApplicationUser)
    .HasForeignKey(sc => sc.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>()
    .HasMany(u => u.BankAccounts)
    .WithOne(sc => sc.ApplicationUser)
    .HasForeignKey(sc => sc.ApplicationUserId);
        }


        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer" },
                new IdentityRole() { Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "Manager" },
                new IdentityRole() { Name = "Staff", ConcurrencyStamp = "3", NormalizedName = "Staff" });
        }

        private static void SeedStatus(ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(
                new Status() {Id = 1, StatusState = "Pending" },
                new Status() { Id = 2, StatusState = "Approved" },
                new Status() {Id = 3, StatusState = "Processing" },
                new Status() {Id = 4, StatusState = "Shipped" },
                new Status() {Id = 5, StatusState = "Payonline-approved" },
                new Status() {Id = 6, StatusState = "COD" },
                new Status() {Id = 7, StatusState = "Payonline" });


        }

    }
}
