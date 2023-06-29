using System.Collections.Generic;
using System.Linq;
using CarFish.Shared.DbContext;
using CarFish.Shared.Models;

namespace CarFish.Models
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly AppDbContext _appDbContext;
        public ImagesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Images> GetImagesByProductId(int productId)
        {
            return _appDbContext.Images.Where(i => i.ProductID == productId);
        }
    }
}
