using CarFish.Models;
using Microsoft.AspNetCore.Mvc;
namespace CarFish.UI.Areas.Datalex.Controllers
{
    [Area("Datalex")]
    public class ShoppingCartController: Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            return View();
        }

        public IActionResult AddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.GetProductById(productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return NoContent();
        }

        //public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        //{
        //    var selectedPie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

        //    if (selectedPie != null)
        //    {
        //        _shoppingCart.RemoveFromCart(selectedPie);
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
