using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetEd.CoreAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetEd.CoreAdmin.ViewComponents
{
    public class CoreAdminMenuViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEnumerable<DiscoveredDbContextType> dbContexts;
        private bool isDatalex = false;

        private readonly List<string> EntitiesToShow = new List<string>
        {
            "Home",
            "Images",
            "Products",
            "Categories",
            
        };

        private readonly List<string> EntitiesToShowDatalex = new List<string>
        {
            "Home",
            "ImagesD",
            "ProductsD",
            "CategoriesD",
        };
        public CoreAdminMenuViewComponent(IEnumerable<DiscoveredDbContextType> dbContexts, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContexts = dbContexts;
            _contextAccessor = httpContextAccessor;
            var loggedInUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            if (loggedInUser != "greenback")
                isDatalex = true;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new MenuViewModel();

            foreach(var dbContext in dbContexts)
            {
                viewModel.DbContextNames.Add(dbContext.Type.Name);

                foreach(var dbSetProperty in dbContext.Type.GetProperties())
                {
                    // looking for DbSet<Entity>
                    if (dbSetProperty.PropertyType.IsGenericType && dbSetProperty.PropertyType.Name.StartsWith("DbSet"))
                    {
                        if (isDatalex)
                        {
                            if (EntitiesToShowDatalex.Contains(dbSetProperty.Name))
                            {
                                viewModel.DbSetNames.Add(dbSetProperty.Name);
                            }
                        }
                        else
                        {
                            if (EntitiesToShow.Contains(dbSetProperty.Name))
                            {
                                viewModel.DbSetNames.Add(dbSetProperty.Name);
                            }
                        }
                    }    
                }
            }

            return View(viewModel);
        }
    }
}
