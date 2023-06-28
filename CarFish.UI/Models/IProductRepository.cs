using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public interface IProductRepository
    {
        //IEnumerable<Product> GetSinglePageProducts { get; }

        IEnumerable<Product> GetSinglePageProducts(int page = 1);
        IEnumerable<Product> GetFeaturedProducts { get; }
        IEnumerable<Product> GetRecentProducts { get; }
        Product GetProductById(int productId);

        public float  GetMaximumProductsAmount();
    }
}
