using System.ComponentModel.DataAnnotations;

namespace CarFish.Shared.Models.Datalex
{
    public class ShoppingCartItemDatalex
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public ProductDatalex Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
