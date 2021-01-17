using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFish.Models
{
    public class MockCategoryRepository:ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
                new Category{ CategoryId=1,CategoryName="ელექტრო ნასოსები", Description="ელექტრო ნასოსების ინფორმაცია"},
                new Category{ CategoryId=2,CategoryName="ჩაიდნები", Description="ჩაიდნების ინფორმაცია"},
                new Category{ CategoryId=3,CategoryName="ანკესები", Description="ანკესების ინფორმაცია"}
            };
    }
}
