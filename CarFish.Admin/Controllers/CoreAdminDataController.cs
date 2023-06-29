using DotNetEd.CoreAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarFish.Shared.DbContext;
using CarFish.Shared.Models;

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
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult Index(string id)
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
            var dbSetValue = GetDbSetValueOrNull(id, out var dbContextObject, out var entityType);

            var newEntity = System.Activator.CreateInstance(entityType);
            ViewBag.DbSetName = id;

            return View(newEntity);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult EditEntity(string dbSetName, string id)
        {
            var entityToEdit = GetEntityFromDbSet(dbSetName, id, out var dbContextObject, out var entityType);
            ViewBag.DbSetName = dbSetName;
            ViewBag.Id = id;

            var dbContext = (AppDbContext)HttpContext.RequestServices.GetRequiredService(dbContexts.First().Type);
            
            if (entityToEdit is Product product)
            {
                var productss = dbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == int.Parse(id));
                ViewBag.Images = dbContext.Images.Where(i => i.ProductID == int.Parse(id)).ToList();

                ViewBag.Category = dbContext.Categories.FirstOrDefault(i => i.Id == product.Category.Id)?.Name ?? "";
            }

            return View("Edit", entityToEdit);
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
                    var dbContext = (AppDbContext)HttpContext.RequestServices.GetRequiredService(dbContexts.First().Type);
                    var images = dbContext.Images.Where(i => i.ProductID == int.Parse(id)).ToList();

                    foreach (var image in images)
                        dbContext.Remove(image);

                    dbContext.SaveChanges();
                    //Then add
                    //Images code

                    if (ProductImages != null)
                    {
                        if (ProductImages.Split(',') != null)
                        {

                            string[] images_array = ProductImages.Split(',');
                            var dbSetValue2 = GetDbSetValueOrNull("Images", out var dbContextObject2, out var entityType2);
                            var newEntity2 = Activator.CreateInstance(entityType2);
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
            var viewModel = new DataDeleteViewModel();
            viewModel.DbSetName = dbSetName;
            viewModel.Id = id;
            viewModel.Object = GetEntityFromDbSet(dbSetName, id, out var dbContext, out var entityType);
            if (viewModel.Object == null) return NotFound();

            return View(viewModel);
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
}