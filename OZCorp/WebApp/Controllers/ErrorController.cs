using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Common;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult Message()
        {   
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var response = new Response
            {
                Success = false,
                Message = feature?.Error?.Message
            };
            return Json(response);
        }
    }
}