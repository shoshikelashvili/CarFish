using System.Collections.Generic;
using System.Linq;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public class MockProductRepository: IProductRepository
    {
        public IEnumerable<Product> GetSinglePageProducts(int page = 1)
        {
            return new List<Product>
            {
                new Product{ ProductId=1, Name="ელექტრო ნასოსი", Price = 12, ShortDescription= "ელექტრო ნასოსი მოკლედ"},
                new Product{ ProductId=1, Name="ჩაიდანი", Price = 14, ShortDescription= "ჩაიდანი მოკლედ"},
                new Product{ ProductId=1, Name="ანკესი", Price = 16, ShortDescription= "ანკესი მოკლედ"}
            };
        }
            

        public IEnumerable<Product> GetFeaturedProducts =>
            new List<Product>
            {
                new Product{ ProductId=1, Name="მანქანის გადასაფარებელი", Price = 12, ShortDescription= "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში", ImageThumbnailUrl="https://i.imgur.com/jDlwQfT.png"},
                new Product{ ProductId=1, Name="ჩაიდანი", Price = 14, ShortDescription= "ჩაიდანი მოკლედ რაღაც ინფორმაცია", ImageThumbnailUrl="https://i.imgur.com/jM45dWU.png"},
                new Product{ ProductId=1, Name="ანკესი", Price = 16, ShortDescription= "ანკესი მოკლედ", ImageThumbnailUrl="https://i.imgur.com/AIwoQYN.png"}
            };

        public IEnumerable<Product> GetRecentProducts =>
            new List<Product>
            {
                new Product{ ProductId=1, Name="მანქანის გადასაფარებელი", Price = 12, ShortDescription= "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში", ImageThumbnailUrl="https://i.imgur.com/jDlwQfT.png", IsFeatured=true},
                new Product{ ProductId=1, Name="ჩაიდანი", Price = 14, ShortDescription= "ჩაიდანი მოკლედ რაღაც ინფორმაცია", ImageThumbnailUrl="https://i.imgur.com/jM45dWU.png",IsFeatured=true},
                new Product{ ProductId=1, Name="ანკესი", Price = 16, ShortDescription= "ანკესი მოკლედ", ImageThumbnailUrl="https://i.imgur.com/AIwoQYN.png",IsFeatured=true},
                new Product{ ProductId=1, Name="მანქანის გადასაფარებელი", Price = 12, ShortDescription= "მანქანის 'ჩიხოლები' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში", ImageThumbnailUrl="https://i.imgur.com/jDlwQfT.png"},
                new Product{ ProductId=1, Name="ჩაიდანი", Price = 14, ShortDescription= "ჩაიდანი მოკლედ რაღაც ინფორმაცია", ImageThumbnailUrl="https://i.imgur.com/jM45dWU.png"},
                new Product{ ProductId=1, Name="ანკესი", Price = 16, ShortDescription= "ანკესი მოკლედ", ImageThumbnailUrl="https://i.imgur.com/AIwoQYN.png"}
            };

        public Product GetProductById(int productId)
        {
            return GetSinglePageProducts().FirstOrDefault(p => p.ProductId == productId);
        }

        public float GetMaximumProductsAmount()
        {
            return 2;
        }
    }
}
