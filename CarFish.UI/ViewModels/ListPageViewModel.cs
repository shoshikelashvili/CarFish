using System.Collections.Generic;
using CarFish.Shared.Models;

namespace CarFish.ViewModels
{
    public class ListPageViewModel
    {
        public IEnumerable<Product> products { get; set; }

        public ShoppingCartViewModel shoppingCartViewModel { get; set; }

        public int page { get; set; }

        public float maxPages { get; set; }
        public Category? category { get; set; } = null;
    }
}
