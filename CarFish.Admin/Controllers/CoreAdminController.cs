using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetEd.CoreAdmin.Controllers
{
    [CoreAdminAuth]
    public class CoreAdminController : Controller
    {
        [IgnoreAntiforgeryToken]
        public IActionResult Index()
        {
            string test = Request.Cookies["is_admin"];
            if(test == "true")
            {
                return View();
            }
            else
            {
                return Redirect("/404"); ;
            }
            
        }
    }
}
