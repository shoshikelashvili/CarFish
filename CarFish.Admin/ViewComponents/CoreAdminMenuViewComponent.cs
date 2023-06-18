using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetEd.CoreAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetEd.CoreAdmin.ViewComponents
{
    public class CoreAdminMenuViewComponent : ViewComponent
    {
        private readonly IEnumerable<DiscoveredDbContextType> dbContexts;

        private readonly List<string> EntitiesToShow = new List<string>
        {
            "Home",
            "Images",
            "Products"
        };
        public CoreAdminMenuViewComponent(IEnumerable<DiscoveredDbContextType> dbContexts)
        {
            this.dbContexts = dbContexts;
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
                        if(EntitiesToShow.Contains(dbSetProperty.Name))
                            viewModel.DbSetNames.Add(dbSetProperty.Name);
                    }    
                }
            }

            return View(viewModel);
        }
    }
}
