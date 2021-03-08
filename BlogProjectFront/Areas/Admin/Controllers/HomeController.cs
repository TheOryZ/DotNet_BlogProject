using BlogProjectFront.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.Areas.Admin.Controllers 
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [JWtAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}