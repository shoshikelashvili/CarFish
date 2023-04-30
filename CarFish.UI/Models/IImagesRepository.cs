using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public interface IImagesRepository
    {
        IEnumerable<Images> GetImagesByProductId(int productId);
    }
}
