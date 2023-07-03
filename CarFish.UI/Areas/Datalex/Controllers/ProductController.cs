using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarFish.Models;
using CarFish.ViewModels;

namespace CarFish.UI.Areas.Datalex.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly ShoppingCart _shoppingCart;

        public ProductController(IProductRepository productRepository, IImagesRepository imagesRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _imagesRepository = imagesRepository;
            _shoppingCart = shoppingCart;
        }
        
        public IActionResult Details(int id)
        {
            DetailsPageViewModel detailsPageViewModel = new DetailsPageViewModel();
            detailsPageViewModel.product = _productRepository.GetProductById(id);
            detailsPageViewModel.images = _imagesRepository.GetImagesByProductId(id);
            detailsPageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            detailsPageViewModel.shoppingCartViewModel.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            detailsPageViewModel.shoppingCartViewModel.ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal();
            if (detailsPageViewModel.product == null) return NotFound();
            return View(detailsPageViewModel);
        }

        [HttpGet]
        [Route("Product/List/{page?}")]
        public IActionResult List(int page = 1, int category = 0, string orderBy = "idDesc")
        {
            ListPageViewModel listPageViewModel = new ListPageViewModel();
            listPageViewModel.products = _productRepository.GetSinglePageProducts(page, category, orderBy);
            listPageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            listPageViewModel.shoppingCartViewModel.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            listPageViewModel.shoppingCartViewModel.ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal();
            listPageViewModel.page = page;
            listPageViewModel.maxPages = _productRepository.GetMaximumProductsAmount(category) / 6;
            if (category != 0)
                listPageViewModel.category = _productRepository.GetCategoryById(category);

            listPageViewModel.orderBy = orderBy;
            return View(listPageViewModel);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            return View(_productRepository.GetCategories().ToList());
        }
    }
}
