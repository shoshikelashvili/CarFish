using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _appDbContext.Products;
            }
        }

        public IEnumerable<Product> GetFeaturedProducts
        {
            get
            {
                return _appDbContext.Products.Where(p => p.IsFeatured);
            }
        }

        public IEnumerable<Product> GetRecentProducts
        {
            get
            {
                return _appDbContext.Products.OrderByDescending(p => p.ProductId).Take(6);
            }
        }

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
