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
                IsFeatured = true
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "ჩაიდანი",
                Price = 14,
                ShortDescription = "ჩაიდანი მოკლედ რაღაც ინფორმაცია",
                ImageThumbnailUrl = "https://i.imgur.com/jM45dWU.png",
                IsFeatured = true
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "ანკესი",
                Price = 16,
                ShortDescription = "ანკესი მოკლედ",
                ImageThumbnailUrl = "https://i.imgur.com/AIwoQYN.png",
                IsFeatured = true
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "მანქანის გადასაფარებელი",
                Price = 12,
                ShortDescription = "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში",
                ImageThumbnailUrl = "https://i.imgur.com/jDlwQfT.png",
                IsFeatured = true
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "ჩაიდანი",
                Price = 14,
                ShortDescription = "ჩაიდანი მოკლედ რაღაც ინფორმაცია",
                ImageThumbnailUrl = "https://i.imgur.com/jM45dWU.png",
                IsFeatured = true
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "ანკესი",
                Price = 16,
                ShortDescription = "ანკესი მოკლედ",
                ImageThumbnailUrl = "https://i.imgur.com/AIwoQYN.png",
                IsFeatured = true
            });
        }
    }
}
