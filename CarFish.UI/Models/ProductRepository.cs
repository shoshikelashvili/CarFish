using System.Collections.Generic;
using System.Linq;
using CarFish.Shared.DbContext;
using CarFish.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFish.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetSinglePageProducts(int page = 1, int categoryId = 0)
        {
            return categoryId == 0
                ? _appDbContext.Products.Include(p => p.Category)
                    .OrderByDescending(p => p.ProductId)
                    .Skip((page - 1) * 6)
                    .Take(6)
                : _appDbContext.Products.Include(p => p.Category)
                    .Where(p => p.Category.Id == categoryId)
                    .OrderByDescending(p => p.ProductId)
                    .Skip((page - 1) * 6)
                    .Take(6);
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
            return _appDbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _appDbContext.Categories.First(p => p.Id == id);
        }

        public float GetMaximumProductsAmount(int categoryId = 0)
        {
            return categoryId == 0
                ? _appDbContext.Products.Count(p => p.ProductId > 0)
                : _appDbContext.Products.Count(p => p.ProductId > 0 && p.Category.Id == categoryId);
        }
    }
}
