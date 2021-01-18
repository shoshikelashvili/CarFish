using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> GetFeaturedProducts { get; }
        IEnumerable<Product> GetRecentProducts { get; }
        Product GetProductById(int productId);
    }
}
