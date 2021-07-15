using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarFish.Models;
using CarFish.ViewModels;

namespace CarFish.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        public HomeController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.FeaturedProducts = _productRepository.GetFeaturedProducts;
            homePageViewModel.RecentProducts = _productRepository.GetRecentProducts;
            homePageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            homePageViewModel.shoppingCartViewModel.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            homePageViewModel.shoppingCartViewModel.ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal();
            return View(homePageViewModel);
        }

        public IActionResult ContactUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
