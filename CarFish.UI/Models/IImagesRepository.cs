using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public interface IImagesRepository
    {
        IEnumerable<Images> GetImagesByProductId(int productId);
    }
}
