using System;
using CarFish.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFish.Shared.Models;

namespace CarFish.ViewModels
{
    public class DetailsPageViewModel
    {
        public Product product { get; set; }
        public IEnumerable<Images> images { get; set; }
        
        public ShoppingCartViewModel shoppingCartViewModel { get; set; }
    }
}
