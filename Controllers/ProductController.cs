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
    public class ProductController: Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IImagesRepository _imagesRepository;

        public ProductController(IProductRepository productRepository, IImagesRepository imagesRepository)
        {
            _productRepository = productRepository;
            _imagesRepository = imagesRepository;
        }

        public IActionResult Details(int id)
        {
            DetailsPageViewModel detailsPageViewModel = new DetailsPageViewModel();
            detailsPageViewModel.product = _productRepository.GetProductById(id);
            detailsPageViewModel.images = _imagesRepository.GetImagesByProductId(id);
            if (detailsPageViewModel.product == null) return NotFound();
            return View(detailsPageViewModel);
        }

        public IActionResult List()
        {
            IEnumerable<Product> products = _productRepository.AllProducts;
            return View(products);
        }
    }
}
