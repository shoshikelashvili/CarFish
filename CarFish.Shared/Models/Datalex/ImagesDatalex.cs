using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFish.Shared.Models.Datalex
{
    public class ImagesDatalex
    {
        [Key]
        public int ID { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("ProductDatalex")]
        public int ProductID { get; set; }
    }
}
