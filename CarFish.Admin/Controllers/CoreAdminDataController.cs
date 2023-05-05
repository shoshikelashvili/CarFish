using DotNetEd.CoreAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEd.CoreAdmin.Controllers
{
    [CoreAdminAuth]
    public class CoreAdminDataController : Controller
    {
        private readonly IEnumerable<DiscoveredDbContextType> dbContexts;

        public string CustomDbConnString { get; }

        public CoreAdminDataController(IEnumerable<DiscoveredDbContextType> dbContexts)
        {
            this.dbContexts = dbContexts;
            //fix thissss
            this.CustomDbConnString = "server=localhost;port=3306;username=greenback;password=greenb@ckDOTA123;database=carfishg_carfish";
            //this.CustomDbConnString = "server=localhost;port=3306;username=root;password=;database=carfish_dev;charset=utf8";
        }


        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult Index(string id)
        {
            string test = Request.Cookies["is_admin"];
            if (test == "true")
            {
                var viewModel = new DataListViewModel();

                foreach (var dbContext in dbContexts)
                {
                    foreach (var dbSetProperty in dbContext.Type.GetProperties())
                    {
                        if (dbSetProperty.PropertyType.IsGenericType && dbSetProperty.PropertyType.Name.StartsWith("DbSet") && dbSetProperty.Name.ToLowerInvariant() == id.ToLowerInvariant())
                        {
                            viewModel.EntityType = dbSetProperty.PropertyType.GetGenericArguments().First();
                            viewModel.DbSetProperty = dbSetProperty;

                            var dbContextObject = (DbContext)this.HttpContext.RequestServices.GetRequiredService(dbContext.Type);

                            var dbSetValue = dbSetProperty.GetValue(dbContextObject);

                            viewModel.Data = (IEnumerable<object>)dbSetValue;
                            viewModel.DbContext = dbContextObject;
                        }
                    }
                }

                return View(viewModel);
            }
            else
            {
                return Redirect("/404"); ;
            }
        }
        [IgnoreAntiforgeryToken]
        private object GetDbSetValueOrNull(string dbSetName, out DbContext dbContextObject, out Type typeOfEntity)
        {
            foreach (var dbContext in dbContexts)
            {
                foreach (var dbSetProperty in dbContext.Type.GetProperties())
                {
                    if (dbSetProperty.PropertyType.IsGenericType && dbSetProperty.PropertyType.Name.StartsWith("DbSet") && dbSetProperty.Name.ToLowerInvariant() == dbSetName.ToLowerInvariant())
                    {
                        dbContextObject = (DbContext)this.HttpContext.RequestServices.GetRequiredService(dbContext.Type);
                        typeOfEntity = dbSetProperty.PropertyType.GetGenericArguments()[0];
                        return dbSetProperty.GetValue(dbContextObject);
                    }
                }
            }

            dbContextObject = null;
            typeOfEntity = null;
            return null;
        }

        [IgnoreAntiforgeryToken]
        private object GetEntityFromDbSet(string dbSetName, string id, out DbContext dbContextObject, out Type typeOfEntity)
        {
            var dbSetValue = GetDbSetValueOrNull(dbSetName, out dbContextObject, out typeOfEntity);

            var primaryKey = dbContextObject.Model.FindEntityType(typeOfEntity).FindPrimaryKey();
            var clrType = primaryKey.Properties[0].ClrType;

            object convertedPrimaryKey = id;
            if (clrType == typeof(Guid))
            {
                convertedPrimaryKey = Guid.Parse(id);
            }
            else if (clrType == typeof(int))
            {
                convertedPrimaryKey = int.Parse(id);
            }
            else if (clrType == typeof(Int64))
            {
                convertedPrimaryKey = Int64.Parse(id);
            }

            return dbSetValue.GetType().InvokeMember("Find", BindingFlags.InvokeMethod, null, dbSetValue, args: new object[] { convertedPrimaryKey });
        }

        [IgnoreAntiforgeryToken]
        private IQueryable<Images> GetProductImages(string dbSetName, int id, out DbContext dbContextObject, out Type typeOfEntity)
        {
            var dbSetValue = GetDbSetValueOrNull("Images", out dbContextObject, out typeOfEntity);
            var newEntity = System.Activator.CreateInstance(typeOfEntity);

            var connectionstring = this.CustomDbConnString;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySQL(connectionstring);

            IQueryable<Images> buba = null;
            using (AppDbContext carFishDb = new AppDbContext(optionsBuilder.Options))
            {
                buba = carFishDb.Images.Where(i => i.ProductID == id);
                var gela = 5;
                return buba;
            }

            
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateEntityPost(string dbSetName, string id, [FromForm] object formData, string ProductImages)
        {
            var dbSetValue = GetDbSetValueOrNull(dbSetName, out var dbContextObject, out var entityType);
            var newEntity = System.Activator.CreateInstance(entityType);

            if (await TryUpdateModelAsync(newEntity, entityType, string.Empty))
            {
                if (TryValidateModel(newEntity))
                {
                    // updated model with new values
                    dbContextObject.Add(newEntity);
                    await dbContextObject.SaveChangesAsync();

                    var new_id = newEntity.GetType().GetProperty("ProductId").GetValue(newEntity, null);

                    if (new_id != null) //Product has been added
                    {
                        if (ProductImages != null)
                        {
                            string[] images_array = ProductImages.Split(',');

                            var dbSetValue2 = GetDbSetValueOrNull("Images", out var dbContextObject2, out var entityType2);
                            var newEntity2 = System.Activator.CreateInstance(entityType2);
                            foreach (string image in images_array)
                            {
                                newEntity2.GetType().GetProperty("ImageURL").SetValue(newEntity2, image);
                                newEntity2.GetType().GetProperty("ProductID").SetValue(newEntity2, new_id);
                                newEntity2.GetType().GetProperty("ID").SetValue(newEntity2, 0);
                                if (TryValidateModel(newEntity2))
                                {
                                    dbContextObject2.Add(newEntity2);
                                    dbContextObject2.SaveChanges();
                                }
                            }
                        }
                    }

                    return RedirectToAction("Index", new { id = dbSetName });
                }
            }

            ViewBag.DbSetName = id;

            return View("Create", newEntity);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(string id)
        {
            string test = Request.Cookies["is_admin"];
            if (test == "true")
            {
                var dbSetValue = GetDbSetValueOrNull(id, out var dbContextObject, out var entityType);

                var newEntity = System.Activator.CreateInstance(entityType);
                ViewBag.DbSetName = id;

                return View(newEntity);
            }
            else
            {
                return Redirect("/404"); ;
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult EditEntity(string dbSetName, string id)
        {
            string test = Request.Cookies["is_admin"];
            if (test == "true")
            {
                var entityToEdit = GetEntityFromDbSet(dbSetName, id, out var dbContextObject, out var entityType);
                ViewBag.DbSetName = dbSetName;
                ViewBag.Id = id;

                //Code for fetching options
                var connectionstring = this.CustomDbConnString;
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySQL(connectionstring);

                using (AppDbContext carFishDb = new AppDbContext(optionsBuilder.Options))
                {
                    ViewBag.Images = carFishDb.Images.Where(i => i.ProductID == int.Parse(id)).ToList();
                    return View("Edit", entityToEdit);
                }
            }
            else
            {
                return Redirect("/404"); ;
            }
        }



        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> EditEntityPost(string dbSetName, string id, [FromForm] object formData, string ProductImages)
        {
            var entityToEdit = GetEntityFromDbSet(dbSetName, id, out var dbContextObject, out var entityType);

            dbContextObject.Attach(entityToEdit);


            if (await TryUpdateModelAsync(entityToEdit, entityType, string.Empty))
            {
                if (TryValidateModel(entityToEdit))
                {
                    await dbContextObject.SaveChangesAsync();

                    //Delete images

                    var connectionstring = this.CustomDbConnString;
                    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                    optionsBuilder.UseMySQL(connectionstring);

                    using (AppDbContext carFishDb = new AppDbContext(optionsBuilder.Options))
                    {
                        var images = carFishDb.Images.Where(i => i.ProductID == int.Parse(id)).ToList();
                        foreach (Images image in images)
                        {
                            carFishDb.Remove(image);
                        }

                        carFishDb.SaveChanges();
                    }
                    //Then add

                    //Images code

                    if (ProductImages != null)
                    {
                        if (ProductImages.Split(',') != null)
                        {

                            string[] images_array = ProductImages.Split(',');
                            var dbSetValue2 = GetDbSetValueOrNull("Images", out var dbContextObject2, out var entityType2);
                            var newEntity2 = System.Activator.CreateInstance(entityType2);
                            foreach (string image in images_array)
                            {
                                newEntity2.GetType().GetProperty("ImageURL").SetValue(newEntity2, image);
                                newEntity2.GetType().GetProperty("ProductID").SetValue(newEntity2, int.Parse(id));
                                newEntity2.GetType().GetProperty("ID").SetValue(newEntity2, 0);
                                if (TryValidateModel(newEntity2))
                                {
                                    dbContextObject2.Add(newEntity2);
                                    dbContextObject2.SaveChanges();
                                }
                            }
                        }
                    }


                    return RedirectToAction("Index", new { id = dbSetName });
                }
            }

            ViewBag.DbSetName = dbSetName;
            ViewBag.Id = id;

            return View("Edit", entityToEdit);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteEntity(string dbSetName, string id)
        {
            string test = Request.Cookies["is_admin"];
            if (test == "true")
            {
                var viewModel = new DataDeleteViewModel();
                viewModel.DbSetName = dbSetName;
                viewModel.Id = id;
                viewModel.Object = GetEntityFromDbSet(dbSetName, id, out var dbContext, out var entityType);
                if (viewModel.Object == null) return NotFound();

                return View(viewModel);
            }
            else
            {
                return Redirect("/404"); ;
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteEntityPost([FromForm] DataDeleteViewModel viewModel)
        {
            foreach (var dbContext in dbContexts)
            {
                foreach (var dbSetProperty in dbContext.Type.GetProperties())
                {
                    if (dbSetProperty.PropertyType.IsGenericType && dbSetProperty.PropertyType.Name.StartsWith("DbSet") && dbSetProperty.Name.ToLowerInvariant() == viewModel.DbSetName.ToLowerInvariant())
                    {
                        var dbContextObject = (DbContext)this.HttpContext.RequestServices.GetRequiredService(dbContext.Type);
                        var dbSetValue = dbSetProperty.GetValue(dbContextObject);

                        var primaryKey = dbContextObject.Model.FindEntityType(dbSetProperty.PropertyType.GetGenericArguments()[0]).FindPrimaryKey();
                        var clrType = primaryKey.Properties[0].ClrType;

                        object convertedPrimaryKey = viewModel.Id;
                        if (clrType == typeof(Guid))
                        {
                            convertedPrimaryKey = Guid.Parse(viewModel.Id);
                        }
                        else if (clrType == typeof(int))
                        {
                            convertedPrimaryKey = int.Parse(viewModel.Id);
                        }
                        else if (clrType == typeof(Int64))
                        {
                            convertedPrimaryKey = Int64.Parse(viewModel.Id);
                        }

                        var entityToDelete = dbSetValue.GetType().InvokeMember("Find", BindingFlags.InvokeMethod, null, dbSetValue, args: new object[] { convertedPrimaryKey });
                        dbSetValue.GetType().InvokeMember("Remove", BindingFlags.InvokeMethod, null, dbSetValue, args: new object[] { entityToDelete });

                        await dbContextObject.SaveChangesAsync();

                    }
                }
            }

            return RedirectToAction("Index", new { Id = viewModel.DbSetName });

        }
    }


    public class Product
    {
        public Product()
        {
            AllImages = new List<Images>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool InStock { get; set; }

        public bool IsFeatured { get; set; }

        public List<Images> AllImages { get; set; }

    }

    public class Images
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }
        public int ProductID { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;port=3306;username=root;password=;database=carfish");
        //}

        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed products


        }
    }

}