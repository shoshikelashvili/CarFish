using System.Collections.Generic;
using System.Linq;
using CarFish.Shared.DbContext;
using CarFish.Shared.Models;
using CarFish.Shared.Models.Datalex;
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

        public IEnumerable<Product> GetSinglePageProducts(int page = 1, int categoryId = 0, string orderBy = null)
        {
            IQueryable<Product> products = _appDbContext.Products.Include(p => p.Category);
            if (categoryId != 0)
                products = products.Where(p => p.Category.Id == categoryId);
            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    case "idAsc":
                        products = products.OrderBy(p => p.ProductId);
                        break;
                    case "idDesc":
                        products = products.OrderByDescending(p => p.ProductId);
                        break;
                    default:
                        products = products.OrderByDescending(p => p.ProductId);
                        break;
                };
            }

            return products.Skip((page - 1) * 6).Take(6);
        }

        public IEnumerable<ProductDatalex> GetSinglePageProductsDatalex(int page = 1, int categoryId = 0, string orderBy = null)
        {
            IQueryable<ProductDatalex> products = _appDbContext.ProductsD.Include(p => p.Category);
            if (categoryId != 0)
                products = products.Where(p => p.Category.Id == categoryId);
            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    case "idAsc":
                        products = products.OrderBy(p => p.ProductId);
                        break;
                    case "idDesc":
                        products = products.OrderByDescending(p => p.ProductId);
                        break;
                    default:
                        products = products.OrderByDescending(p => p.ProductId);
                        break;
                };
            }

            return products.Skip((page - 1) * 6).Take(6);
        }

        public IEnumerable<Product> GetFeaturedProducts
        {
            get
            {
                return _appDbContext.Products.Where(p => p.IsFeatured);
            }
        }

        public IEnumerable<ProductDatalex> GetFeaturedProductsDatalex
        {
            get
            {
                return _appDbContext.ProductsD.Where(p => p.IsFeatured);
            }
        }

        public IEnumerable<Product> GetRecentProducts
        {
            get
            {
                return _appDbContext.Products.OrderByDescending(p => p.ProductId).Take(6);
            }
        }

        public IEnumerable<ProductDatalex> GetRecentProductsDatalex
        {
            get
            {
                return _appDbContext.ProductsD.OrderByDescending(p => p.ProductId).Take(6);
            }
        }

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }

        public ProductDatalex GetProductByIdDatalex(int productId)
        {
            return _appDbContext.ProductsD.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }


        public IEnumerable<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public IEnumerable<CategoryDatalex> GetCategoriesDatalex()
        {
            return _appDbContext.CategoriesD.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _appDbContext.Categories.First(p => p.Id == id);
        }

        public CategoryDatalex GetCategoryByIdDatalex(int id)
        {
            return _appDbContext.CategoriesD.First(p => p.Id == id);
        }


        public float GetMaximumProductsAmount(int categoryId = 0)
        {
            return categoryId == 0
                ? _appDbContext.Products.Count(p => p.ProductId > 0)
                : _appDbContext.Products.Count(p => p.ProductId > 0 && p.Category.Id == categoryId);
        }

        public float GetMaximumProductsAmountDatalex(int categoryId = 0)
        {
            return categoryId == 0
                ? _appDbContext.ProductsD.Count(p => p.ProductId > 0)
                : _appDbContext.ProductsD.Count(p => p.ProductId > 0 && p.Category.Id == categoryId);
        }

        public int GetProductCountByCategory(int categoryId)
        {
            return _appDbContext.Products.Count(p => p.Category.Id == categoryId);
        }

        public int GetProductCountByCategoryDatalex(int categoryId)
        {
            return _appDbContext.ProductsD.Count(p => p.Category.Id == categoryId);
        }
    }
}
