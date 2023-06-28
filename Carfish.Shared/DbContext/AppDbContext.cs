using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using CarFish.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarFish.Shared.DbContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CarFish;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //For mysql compatibility
            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            //modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
            //Seed products

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "მანქანის გადასაფარებელი",
                Price = 12,
                ShortDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში",
                ImageThumbnailUrl = "https://i.imgur.com/jDlwQfT.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            });

            modelBuilder.Entity<Images>().HasData(new Images
            {
                ID = 1,
                ImageURL = "https://i.imgur.com/APSUSkV.jpg",
                ProductID = 1
            });

            modelBuilder.Entity<Images>().HasData(new Images
            {
                ID = 2,
                ImageURL = "https://i.imgur.com/FSTeMK2.jpg",
                ProductID = 1
            });

            modelBuilder.Entity<Images>().HasData(new Images
            {
                ID = 3,
                ImageURL = "https://i.imgur.com/qKxxqUb.jpg",
                ProductID = 1
            });

            modelBuilder.Entity<Images>().HasData(new Images
            {
                ID = 4,
                ImageURL = "https://i.imgur.com/QfIRm5K.jpg",
                ProductID = 1
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "ჩაიდანი",
                Price = 14,
                ShortDescription = "ჩაიდანი მოკლედ რაღაც ინფორმაცია",
                ImageThumbnailUrl = "https://i.imgur.com/jM45dWU.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "ანკესი",
                Price = 16,
                ShortDescription = "ანკესი მოკლედ",
                ImageThumbnailUrl = "https://i.imgur.com/AIwoQYN.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            }); ;

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "მანქანის გადასაფარებელი",
                Price = 12,
                ShortDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში",
                ImageThumbnailUrl = "https://i.imgur.com/jDlwQfT.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "ჩაიდანი",
                Price = 14,
                ShortDescription = "ჩაიდანი მოკლედ რაღაც ინფორმაცია",
                ImageThumbnailUrl = "https://i.imgur.com/jM45dWU.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "ანკესი",
                Price = 16,
                ShortDescription = "ანკესი მოკლედ",
                ImageThumbnailUrl = "https://i.imgur.com/AIwoQYN.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 7,
                Name = "მანქანის გადასაფარებელი",
                Price = 12,
                ShortDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში",
                ImageThumbnailUrl = "https://i.imgur.com/jDlwQfT.png",
                LongDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ",
                IsFeatured = true,
                InStock = true,
            });
        }
    }
}
