using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
            });;

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
