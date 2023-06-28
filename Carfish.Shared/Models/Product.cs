using System.Collections.Generic;

namespace CarFish.Shared.Models
{
    public class Product
    {
        public Product()
        {
            AllImages = new List<Images>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool InStock { get; set; }

        public bool IsFeatured { get; set; }

        public List<Images> AllImages { get; set; }

    }
}
