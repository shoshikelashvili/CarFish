using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarFish.Shared.Models.Datalex
{
    public class ProductDatalex
    {
        public ProductDatalex()
        {
            AllImages = new List<ImagesDatalex>();
        }
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool InStock { get; set; }

        public bool IsFeatured { get; set; }

        public virtual CategoryDatalex Category { get; set; }

        public List<ImagesDatalex> AllImages { get; set; }
    }
}
