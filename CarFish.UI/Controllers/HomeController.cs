using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarFish.Models;
using CarFish.ViewModels;

namespace CarFish.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IMailService _mailService;
        public HomeController(IProductRepository productRepository, ShoppingCart shoppingCart, IMailService mailService)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
            _mailService = mailService;
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

        public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Problem");
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
