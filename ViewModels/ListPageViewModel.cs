using CarFish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.ViewModels
{
    public class ListPageViewModel
    {
        public IEnumerable<Product> products { get; set; }

        public ShoppingCartViewModel shoppingCartViewModel { get; set; }

        public int page { get; set; }

        public float maxPages { get; set; }
    }
}
