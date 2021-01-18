using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public class Images
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
    }
}
