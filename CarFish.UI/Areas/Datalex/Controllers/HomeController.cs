using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarFish.Models;
using CarFish.ViewModels;

namespace CarFish.UI.Areas.Datalex.Controllers
{
    [Area("Datalex")]
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

            ViewBag.Datalex = true;

            return View("~/Views/Home/Index.cshtml",homePageViewModel);
        }

        public IActionResult ContactUs()
        {
            ViewBag.Datalex = true;
            return View("~/Views/Home/ContactUs.cshtml");
        }

        public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        {
            ViewBag.Datalex = true;
            try
            {
                await _mailService.SendEmailAsync(request);
                return View("~/Views/Home/SendMail.cshtml");
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("~/Views/Home/Error.cshtml");
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Datalex = true;
            return View("~/Views/Home/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PageNotFound()
        {
            ViewBag.Datalex = true;
            return View("~/Views/Home/PageNotFound.cshtml");
        }
    }
}
