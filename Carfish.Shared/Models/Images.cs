using System.ComponentModel.DataAnnotations.Schema;

namespace CarFish.Shared.Models
{
    public class Images
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
    }
}
