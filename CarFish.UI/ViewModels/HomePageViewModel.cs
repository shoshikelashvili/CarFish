using System.Collections.Generic;
using CarFish.Shared.Models;

namespace CarFish.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<Product> RecentProducts { get; set; }
        public ShoppingCartViewModel shoppingCartViewModel { get; set; }
    }
}
