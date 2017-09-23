using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Common.Common;
using Project.Common.Extensions;
using Project.Common.Filters;
using Project.Entities.Global;
using Project.Entities.Identity;
using WebApp.Controllers;
using WebApp.Data;

namespace WebApp.Areas.Manage.Controllers
{
    [Authorize(Policy = Config.MainPolicy)]
    [Area("Manage")]
    public class HomeSettingController : CommonController<HomeSettingController>
    {

        public HomeSettingController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Ads()
        {
            var data = Context.GlobalImages
                .Where(w => w.IsActive)
                .OrderBy(o=>o.Title)
                .ThenBy(o=>o.SubTitle)
                .ToList();
            return Json(data);
        }
        [AllowAnonymous]
        public IActionResult CanEdit()
        {
            var response = new Response {Success = SignInManager.IsSignedIn(User) && User.IsInRole("Administrator")};
            return Json(response);
        }
        public IActionResult List(CommonFilter<string> gridFilter)
        {
            var data = Context.GlobalImages
                .Where(w =>
                string.IsNullOrEmpty(gridFilter.Filter)
                || w.Title.Contains(gridFilter.Filter)
                || w.SubTitle.Contains(gridFilter.Filter)
                )
                .ToResponse(gridFilter);
            return Json(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GlobalImage globalImage, IList<IFormFile> imageUpload)
        {
            globalImage.FileLocation = imageUpload.Any()
                ? imageUpload.ImageUpload(HostingEnv.WebRootPath,false).First().Location
                : null;
            Context.Add(globalImage);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(int id,GlobalImage globalImage, IList<IFormFile> imageUpload)
        {
            var current = Context.GlobalImages.FirstOrDefault(a => a.Id == id);
            if (current != null)
            {
                if (string.IsNullOrEmpty(current.FileLocation))
                {
                    if (System.IO.File.Exists(HostingEnv.WebRootPath + current.FileLocation))
                    {
                        System.IO.File.Delete(HostingEnv.WebRootPath + current.FileLocation);
                    }
                }
                current.Title = globalImage.Title;
                current.SubTitle = globalImage.SubTitle;
                current.IsActive = globalImage.IsActive;
                if (imageUpload.Any())
                {
                    current.FileLocation = imageUpload.Any()
                        ? imageUpload.ImageUpload(HostingEnv.WebRootPath, false).First().Location
                        : null;
                }
                Context.Update(current);
                Context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(long id)
        {
            var current = Context.GlobalImages.FirstOrDefault(f => f.Id == id);
            if (current != null)
            {
                if (string.IsNullOrEmpty(current.FileLocation))
                {
                    if (System.IO.File.Exists(HostingEnv.WebRootPath + current.FileLocation))
                    {
                        System.IO.File.Delete(HostingEnv.WebRootPath + current.FileLocation);
                    }
                }
                Context.Remove(current);
                Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}