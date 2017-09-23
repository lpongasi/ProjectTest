using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Common.Common;
using Project.Common.Extensions;
using Project.Entities.Identity;
using WebApp.Common;
using Project.Common.Enums;
using Project.Common.Filters;
using Project.Entities.Alert;

namespace WebApp.Controllers
{
    public class CommonController<T> : Controller
    {
        protected readonly UserManager<ApplicationUser> UserManager;
        protected readonly SignInManager<ApplicationUser> SignInManager;
        protected readonly RoleManager<Role> Role;
        protected readonly ApplicationDbContext Context;
        protected readonly ILogger Logger;
        protected readonly AppSettings AppSettings;
        protected readonly IHostingEnvironment HostingEnv;
        public CommonController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<Role> role,
          ApplicationDbContext context,
          ILoggerFactory loggerFactory,
          IHostingEnvironment hostingEnv,
          IOptions<AppSettings> appSettings)
        {
            UserManager = userManager;
            Role = role;
            Context = context;
            Logger = loggerFactory.CreateLogger<T>();
            HostingEnv = hostingEnv;
            AppSettings = appSettings.Value;
            SignInManager = signInManager;
        }
        public IActionResult EmailCheckExists(string email)
        {
            try
            {
                return Json(!Context.Users.Any(a => a.Email.Equals(email)));
            }
            catch (Exception)
            {
                return Json(true);
            }
        }
        public IActionResult BarcodeCheckExists(string barcode)
        {
            try
            {
                return Json(!Context.Item.Any(a => a.Barcode.Equals(barcode)));
            }
            catch (Exception)
            {
                return Json(true);
            }
        }
        public IActionResult SiteMap()
        {
            return PartialView();
        }
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserHistory(string id, CommonFilter<string> gridFilter)
            => Json(await Context.UserInfoHistories.Include(u => u.ActionUser)
                .Where(w => w.UserId.Equals(id)
                && (
                string.IsNullOrEmpty(gridFilter.Filter)
                || w.ActionUser.FirstName.Contains(gridFilter.Filter)
                || w.ActionUser.LastName.Contains(gridFilter.Filter)
                || w.Action.Contains(gridFilter.Filter)
                ))
                .Select(s => new
                {
                    s.Id,
                    s.ActionUserId,
                    fullName = s.ActionUser.FullName,
                    s.ActionDate,
                    actionDateString = string.Format("{0:g}", s.ActionDate),
                    s.Action
                })
                .OrderByDescending(o => o.ActionDate)
                .ToResponseAsync(gridFilter)
                );
        [HttpPost]
        public async Task<IActionResult> ItemHistory(long id, CommonFilter<string> gridFilter)
    => Json(await Context.ItemHistories.Include(u => u.ActionUser)
        .Where(w => w.ItemId == id
        && (
        string.IsNullOrEmpty(gridFilter.Filter)
        || w.ActionUser.FirstName.Contains(gridFilter.Filter)
        || w.ActionUser.LastName.Contains(gridFilter.Filter)
        || w.Action.Contains(gridFilter.Filter)
        ))
        .Select(s => new
        {
            s.Id,
            s.ActionUserId,
            fullName = s.ActionUser.FullName,
            s.ActionDate,
            actionDateString = string.Format("{0:g}", s.ActionDate),
            s.Action
        })
        .OrderByDescending(o => o.ActionDate)
        .ToResponseAsync(gridFilter)
        );
        public IActionResult Categories()
        {
            var result = new ResponseList<Option>();
            try
            {
                result.Data =
                    Context.Category.Select(c => new Option(c.Id, c.Name))
                        .OrderBy(o => o.Value)
                        .ToList();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return Json(result);
        }
        public IActionResult SubCategories(int? id)
        {
            var result = new ResponseList<Option>();
            try
            {
                result.Data =
                    Context.SubCategory.Where(w => w.CategoryId == id).Select(c => new Option(c.Id, c.Name))
                        .OrderBy(o => o.Value)
                        .ToList();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return Json(result);
        }
        public IActionResult Unit()
        {
            var result = new ResponseList<Option>();
            try
            {
                result.Data =
                    Context.UnitOfMeasure.Select(c => new Option(c.Id, c.Name))
                        .OrderBy(o => o.Value)
                        .ToList();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return Json(result);
        }
        public IActionResult Size()
        {
            var result = new ResponseList<Option>();
            try
            {
                result.Data =
                    Context.Sizes.Select(c => new Option(c.Id, c.Name))
                        .OrderBy(o => o.Value)
                        .ToList();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return Json(result);
        }

        public IActionResult NotifViewed(long id)
        {
            var userId = UserManager.GetUserId(User);
            var notif = Context.UserNotification.FirstOrDefault(w => w.NotificationId == id && w.UserId.Equals(userId));
            if (notif != null)
            {
                notif.IsViewed = true;
                Context.UserNotification.Update(notif);
                Context.SaveChanges();
            }
            return Json(new { success = true });
        }
        public IActionResult NotificationList(CommonFilter<string> gridFilter)
        {
            var userId = UserManager.GetUserId(User);
            var notif = Context.UserNotification
                .Include(i => i.Notification)
                .Where(u => u.UserId.Equals(userId) && (string.IsNullOrEmpty(gridFilter.Filter) || u.Notification.Title.Contains(gridFilter.Filter)))
                .OrderBy(o => o.IsViewed)
                .ThenByDescending(o => o.Notification.NotifDate)
                .Select(s => new
                {
                    Id = s.NotificationId,
                    s.IsViewed,
                    s.Notification.Title,
                    NotifDate = s.Notification.NotifDate.ToString("MMM dd, yyyy h:mm:ss tt"),
                    s.Notification.Url
                })
                .ToResponse(gridFilter);
            return Json(notif);
        }
        [AllowAnonymous]
        public IActionResult NotificationCount()
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { counts = 0 });
            var userId = UserManager.GetUserId(User);
            var counts = Context.UserNotification
                .Count(u => u.UserId.Equals(userId) && !u.IsViewed);
            return Json(new { counts });
        }
        public void Notify(string title, string url, string commonId, params string[] userIds)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(url) || !userIds.Any())
                return;
            var notif = Context.Notification.Where(w => w.CommonId.Equals(commonId)).ToList();
            if (notif.Any())
                Context.RemoveRange(notif);
            var notification = new Notification
            {
                Title = title,
                Url = url,
                CommonId = commonId,
                NotifDate = DateTime.Now,
                UserNotifications = userIds.Distinct().Select(s => new UserNotification
                {
                    UserId = s
                }).ToList()
            };
            Context.Add(notification);
            Context.SaveChanges();
        }
        #region Helpers

        public async Task<string[]> GetUserIdsForRoles(params string[] roles)
        {
            var userIds = new List<string>();
            foreach (var role in roles)
            {
                var user = await UserManager.GetUsersInRoleAsync(role);
                userIds.AddRange(user.Select(s => s.Id));
            }
            return userIds.ToArray();
        }
        public bool PoEditable(PoStatus status)
            => ((User.IsInRole("Administrator") || User.IsInRole("OfficeClerk")) && status == PoStatus.Purchase)
               || (User.IsInRole("Administrator") && status == PoStatus.Approve);

        public bool ItemAvailable(long id)
        => Context.Item.Any(a => !a.NotForSale && !a.Removed && a.Id == id);
        public bool ItemAvailable(long id, int quantity)
        => Context.Item.Any(a => !a.NotForSale && !a.Removed && a.Id == id && (a.Qty >= quantity || a.PurPro));


        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        protected IEnumerable<Role> GetAllRoles()
            => Context.Roles.Where(r => !r.NormalizedName.Equals("ADMINISTRATOR")).ToList() ?? new List<Role>();
        protected IEnumerable<Role> GetAllRolesCurrent()
            => Context.Roles
            .Include(i => i.Users)
            .Where(w => w.Users.Any(a => a.UserId.Equals(UserManager.GetUserId(User)))).ToList() ?? new List<Role>();
        protected IEnumerable<Role> GetAllRoles(string userId)
            => Context.Roles
            .Include(i => i.Users)
            .Where(w => w.Users.Any(a => a.UserId.Equals(userId))).ToList() ?? new List<Role>();
        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            return UserManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
    [Authorize]
    public class CommonController : CommonController<CommonController>
    {
        public CommonController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
    }
}