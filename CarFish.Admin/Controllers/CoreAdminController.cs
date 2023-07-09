using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetEd.CoreAdmin.Controllers
{
    [CoreAdminAuth]
    public class CoreAdminController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CoreAdminController(IHttpContextAccessor httpContextAccessor) : base()
        {
            _contextAccessor = httpContextAccessor;
        }

        [IgnoreAntiforgeryToken]
        public IActionResult Index()
        {
            var loggedinUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            return View();
        }
    }
}
