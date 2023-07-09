using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarFish.Models;
using CarFish.Shared.Models;
using CarFish.ViewModels;

namespace CarFish.UI.Areas.Datalex.Controllers
{
    [Area("Datalex")]
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IMailService _mailService;
        public HomeController(ProductRepository productRepository, ShoppingCart shoppingCart, IMailService mailService)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            
            homePageViewModel.FeaturedProducts = _productRepository.GetFeaturedProductsDatalex.Select(x => new Product()
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

            homePageViewModel.RecentProducts = _productRepository.GetRecentProductsDatalex.Select(x => new Product()
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

            homePageViewModel.shoppingCartViewModel = new ShoppingCartViewModel();
            homePageViewModel.shoppingCartViewModel.ShoppingCartItems = new List<ShoppingCartItem>();
            homePageViewModel.shoppingCartViewModel.ShoppingCartTotal = 0;

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
