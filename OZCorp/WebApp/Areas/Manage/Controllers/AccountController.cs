using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Controllers;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Project.Entities.Identity;
using Project.Entities.User;
using Project.Models.Account;
using Project.Models.Manage;
using WebApp.Common;
using WebApp.Extensions;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Policy = Config.MainPolicy)]
    public class AccountController : CommonController<AccountController>
    {
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }

        [HttpGet]
        public IActionResult Password()
        {
            return View();
        }
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Password", model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    Logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                AddErrors(result);
                return View("Password", model);
            }
            return RedirectToAction("Index","Home",new { area ="" });
        }

        [HttpGet]
        public IActionResult Info()
        {
            var user = Context.Users.Where(u => u.Id.Equals(UserManager.GetUserId(User))).Select(s => new UpdateUserViewModel
            {
                Id = s.Id,
                Email = s.Email,
                UserId = s.UserId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                MiddleName = s.MiddleName,
                CompanyName = s.CompanyName,
                CompanyContact = s.CompanyContact,
                CompanyAddress = s.CompanyAddress
            }).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = await Context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var currentUser = await GetCurrentUserAsync();
                        var currentUserInfo = Context.UserInfo.First(f => f.Id.Equals(currentUser.Id));

                        currentUser.UserUpdate(user);
                        currentUserInfo.Update(user);

                        Context.Update(currentUser);
                        Context.Update(currentUserInfo);

                        Context.UserInfoHistories.Add(new UserInfoHistory
                        {
                            UserId = user.Id,
                            ActionUserId = UserManager.GetUserId(User),
                            Action = "Update Account Info",
                            ActionDate = DateTime.Now
                        });
                        Context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index","Home",new { area = ""});
        }
    }
}