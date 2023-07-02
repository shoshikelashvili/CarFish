using System.Collections.Generic;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public interface IProductRepository
    {
        //IEnumerable<Product> GetSinglePageProducts { get; }
        IEnumerable<Product> GetSinglePageProducts(int page = 1, int categoryId = 0);
        IEnumerable<Product> GetFeaturedProducts { get; }
        IEnumerable<Product> GetRecentProducts { get; }
        Product GetProductById(int productId);
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        public float GetMaximumProductsAmount(int categoryId = 0);
        int GetProductCountByCategory(int categoryId);
    }
}
