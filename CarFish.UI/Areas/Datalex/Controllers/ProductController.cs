using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using CarFish.Models;
using CarFish.Shared.Models;
using CarFish.ViewModels;

namespace CarFish.UI.Areas.Datalex.Controllers
{
    [Area("Datalex")]
    public class ProductController: Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ImagesRepository _imagesRepository;
        private readonly ShoppingCart _shoppingCart;

        public ProductController(ProductRepository productRepository, ImagesRepository imagesRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _imagesRepository = imagesRepository;
            _shoppingCart = shoppingCart;
        }
        
        public IActionResult Details(int id)
        {
            //this should be moved to a controller action filter
            ViewBag.Datalex = true;
            DetailsPageViewModel detailsPageViewModel = new DetailsPageViewModel();
            var datalexProduct = _productRepository.GetProductByIdDatalex(id);
            detailsPageViewModel.product = new Product
            {
                Category = new Category
                {
                    Name = datalexProduct.Category?.Name,
                    Id = datalexProduct.Category?.Id ?? 0,
                    ImageUrl = datalexProduct.Category?.ImageUrl,
                    ShowInHomePage = datalexProduct.Category?.ShowInHomePage ?? false
                },
                Name = datalexProduct.Name,
                AllImages = datalexProduct.AllImages.Select(x => new Images
                {
                    ID = x.ID,
                    ImageURL = x.ImageURL,
                    ProductID = x.ProductID
                }).ToList(),
                ImageUrl = datalexProduct.ImageUrl,
                ImageThumbnailUrl = datalexProduct.ImageThumbnailUrl,
                InStock = datalexProduct.InStock,
                IsFeatured = datalexProduct.IsFeatured,
                LongDescription = datalexProduct.LongDescription,
                Price = datalexProduct.Price,
                ProductId = datalexProduct.ProductId,
                ShortDescription = datalexProduct.ShortDescription
            };


            detailsPageViewModel.images = _imagesRepository.GetImagesByProductIdDatalex(id).Select(x => new Images
            {
                ProductID = x.ProductID,
                ID = x.ID,
                ImageURL = x.ImageURL
            });
            detailsPageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            detailsPageViewModel.shoppingCartViewModel.ShoppingCartItems = new List<ShoppingCartItem>();
            detailsPageViewModel.shoppingCartViewModel.ShoppingCartTotal = 0;
            if (detailsPageViewModel.product == null) return NotFound();
            return View("~/Views/Product/Details.cshtml", detailsPageViewModel);
        }

        [HttpGet]
        [Route("Datalex/Product/List/{page?}")]
        public IActionResult List(int page = 1, int category = 0, string orderBy = "idDesc")
        {
            ViewBag.Datalex = true;
            ListPageViewModel listPageViewModel = new ListPageViewModel();
            listPageViewModel.products = _productRepository.GetSinglePageProductsDatalex(page, category, orderBy).Select(x => new Product()
            {
                Category = new Category
                {
                    Name = x.Category?.Name,
                    Id = x.Category?.Id ?? 0,
                    ImageUrl = x.Category?.ImageUrl,
                    ShowInHomePage = x.Category?.ShowInHomePage ?? false
                },
                Name = x.Name,
                AllImages = x.AllImages.Select(x => new Images
                {
                    ID = x.ID,
                    ImageURL = x.ImageURL,
                    ProductID = x.ProductID
                }).ToList(),
                ImageUrl = x.ImageUrl,
                ImageThumbnailUrl = x.ImageThumbnailUrl,
                InStock = x.InStock,
                IsFeatured = x.IsFeatured,
                LongDescription = x.LongDescription,
                Price = x.Price,
                ProductId = x.ProductId,
                ShortDescription = x.ShortDescription
            });
            listPageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            listPageViewModel.shoppingCartViewModel.ShoppingCartItems = new List<ShoppingCartItem>();
            listPageViewModel.shoppingCartViewModel.ShoppingCartTotal = 0;
            listPageViewModel.page = page;
            listPageViewModel.maxPages = _productRepository.GetMaximumProductsAmountDatalex(category) / 6;
            if (category != 0)
            {
                var datalexCategory = _productRepository.GetCategoryByIdDatalex(category);
                listPageViewModel.category = new Category
                {
                    Id = datalexCategory.Id,
                    ImageUrl = datalexCategory.ImageUrl,
                    ShowInHomePage =datalexCategory.ShowInHomePage,
                    Name = datalexCategory.Name,
                };
            }

            listPageViewModel.orderBy = orderBy;
            return View("~/Views/Product/List.cshtml", listPageViewModel);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            ViewBag.Datalex = true;
            return View("~/Views/Product/Categories.cshtml", _productRepository.GetCategoriesDatalex().Select(x => new Category
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                ShowInHomePage = x.ShowInHomePage
            }).ToList());
        }
    }
}
