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
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.FeaturedProducts = _productRepository.GetFeaturedProducts;
            homePageViewModel.RecentProducts = _productRepository.GetRecentProducts;
            return View(homePageViewModel);
        }

        //public IActionResult Details(int id)
        //{
        //    DetailsPageViewModel detailsPageViewModel = new DetailsPageViewModel();
        //    detailsPageViewModel.product = _productRepository.GetProductById(id);
        //    detailsPageViewModel.images = _imagesRepository.GetImagesByProductId(id);
        //    if (detailsPageViewModel.product == null) return NotFound();
        //    return View(detailsPageViewModel);
        //}

        //public IActionResult List()
        //{
        //    IEnumerable<Product> products = _productRepository.AllProducts;
        //    return View(products);
        //}
        ////public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
