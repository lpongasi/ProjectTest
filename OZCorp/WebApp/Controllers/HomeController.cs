using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Authorize(Policy = Config.MainPolicy)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult PageNotAvailable()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
