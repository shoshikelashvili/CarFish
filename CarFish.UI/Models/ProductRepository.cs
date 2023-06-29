﻿using System.Collections.Generic;
using System.Linq;
using CarFish.Shared.DbContext;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetSinglePageProducts(int page = 1)
        {
                return _appDbContext.Products.OrderByDescending(p => p.ProductId).Skip((page -1) * 6).Take(6);
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

        public float GetMaximumProductsAmount()
        {
            return _appDbContext.Products.Count(p => p.ProductId > 0);
        }
    }
}
