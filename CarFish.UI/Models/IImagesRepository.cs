using System.Collections.Generic;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public interface IImagesRepository
    {
        IEnumerable<Images> GetImagesByProductId(int productId);
    }
}
