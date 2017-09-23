using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Controllers;
using WebApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Project.Entities.Identity;
using Project.Entities.User;
using Project.Models.Account;
using WebApp.Common;
using Project.Common.Extensions;
using WebApp.Extensions;
using Project.Common.Filters;

namespace WebApp.Areas.Manage.Controllers
{
    //[Authorize(Roles = "Administrator,UserManagement")]
    [Area("Manage"),Authorize(Policy = Config.MainPolicy)]
    public class UserController : CommonController<UserController>
    {
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }

        // GET: Manage/User
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(CommonFilter<string> gridFilter)
        {
            var adminRoleId = Context.Roles.FirstOrDefault(r=>r.NormalizedName.Equals("ADMINISTRATOR"))?.Id;
            var items = await Context.Users
                .Include(i=>i.Roles)
                .Where(w =>
                ((string.IsNullOrEmpty(gridFilter.Filter)
                || w.Email.Contains(gridFilter.Filter)))
                && !w.Roles.Any(a=>a.RoleId.Equals(adminRoleId))
                )
                .Select(s => new
                {
                    s.Id,
                    s.Email,
                    s.FullName,
                    s.UserName,
                    s.CompanyName,
                    s.CompanyAddress,
                    s.CompanyContact,
                    discount = s.Discount.ToPercentage(),
                    tax = s.Tax.ToPercentage()
                })                
                .OrderBy(o => o.CompanyName)
                .ThenBy(o => o.UserName)
                .ThenBy(o => o.Email)
                .ToResponseAsync(gridFilter);

            return Json(items);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewData["Roles"] = GetAllRoles();
            
            var user = Context.Users.Where(u => u.Id.Equals(id)).Select(s => new UpdateUserViewModel
            {
                Id = s.Id,
                Email = s.Email,
                UserId = s.UserId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                MiddleName = s.MiddleName,
                CompanyName = s.CompanyName,
                CompanyContact = s.CompanyContact,
                CompanyAddress = s.CompanyAddress,
                Discount = s.Discount * 100,
                Tax = s.Tax * 100,
                OtherRemarks = s.OtherRemarks,
                OtherFees = s.OtherFees
            }).FirstOrDefault();

            if (user == null)
                return View("Register");
            user.Roles = (GetAllRoles(user.Id) ?? new List<Role>()).Select(s=>s.Id);

            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = await Context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var currentUser = Context.Users.First(f => f.Id.Equals(user.Id));
                        var currentUserInfo = Context.UserInfo.First(f => f.Id.Equals(user.Id));
                        var userRoles = Context.UserRoles.Where(w => w.UserId.Equals(user.Id)).ToList();

                        var removeRole = userRoles.Where(w => user.Roles.All(a => a != w.RoleId));
                        var addedRole = user.Roles.Where(w => userRoles.All(a => a.RoleId != w) && removeRole.All(a => a.RoleId != w)).Select(s => new IdentityUserRole<string>
                        {
                            RoleId = s,
                            UserId = user.Id
                        });

                        if (removeRole.Any())
                            Context.UserRoles.RemoveRange(removeRole);

                        if (addedRole.Any())
                            Context.UserRoles.AddRange(addedRole);
                        
                        currentUser.Update(user);
                        currentUserInfo.Update(user);

                        Context.Update(currentUser);
                        Context.Update(currentUserInfo);

                        Context.UserInfoHistories.Add(new UserInfoHistory {
                            UserId = user.Id,
                            ActionUserId = UserManager.GetUserId(User),
                            Action = "Update Info",
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
            return RedirectToAction("Index");
        }
        //
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Roles"] = GetAllRoles();
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewData["Roles"] = Context.Roles.ToList() ?? new List<Role>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Register(model);
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userInfo = new UserInfo();
                    userInfo.Register(user);
                    Context.UserInfo.Add(userInfo);
                    Context.UserInfoHistories.Add(new UserInfoHistory
                    {
                        UserId = user.Id,
                        ActionUserId = UserManager.GetUserId(User),
                        Action = "Add User",
                        ActionDate = DateTime.Now
                    });

                    await Context.SaveChangesAsync();
                    if (model.Roles.Any())
                        result = await UserManager.AddToRolesAsync(user, model.Roles);
                }

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var userEmail = Context.Users.FirstOrDefault(w => w.Id == id)?.Email;
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index");
            }
            var user = await UserManager.FindByEmailAsync(userEmail);
            if(await UserManager.IsInRoleAsync(user, "Administrator"))
            {
                return RedirectToAction("Index");
            }
            return View(new ResetPasswordViewModel
            {
                Email = userEmail
            });
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null || await UserManager.IsInRoleAsync(user, "Administrator"))
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Index");
            }


            var result = await UserManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await UserManager.AddPasswordAsync(user, model.Password);
                Context.UserInfoHistories.Add(new UserInfoHistory
                {
                    UserId = user.Id,
                    ActionUserId = UserManager.GetUserId(User),
                    Action = "Reset Password",
                    ActionDate = DateTime.Now
                });
                await Context.SaveChangesAsync();
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = Context.Users.Where(u => u.Id.Equals(id)).Select(s => new UpdateUserViewModel
            {
                Id = s.Id,
                Email = s.Email,
                UserId = s.UserId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                MiddleName = s.MiddleName,
                CompanyName = s.CompanyName,
                CompanyContact = s.CompanyContact,
                CompanyAddress = s.CompanyAddress,
                Discount = s.Discount * 100,
                Tax = s.Tax * 100,
                OtherRemarks = s.OtherRemarks,
                OtherFees = s.OtherFees
            }).FirstOrDefault();

            if (user == null)
                return View("Index");
           
            return View(user);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var logins = user.Logins;
                var rolesForUser = await UserManager.GetRolesAsync(user);

                using (var transaction = Context.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        await UserManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    }

                    if (rolesForUser.Any())
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            await UserManager.RemoveFromRoleAsync(user, item);
                        }
                    }

                    await UserManager.DeleteAsync(user);
                    Context.UserInfoHistories.Add(new UserInfoHistory
                    {
                        UserId = user.Id,
                        ActionUserId = UserManager.GetUserId(User),
                        Action = "Remove Account",
                        ActionDate = DateTime.Now
                    });
                    await Context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
