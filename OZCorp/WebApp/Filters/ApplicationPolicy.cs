using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities.Identity;
using Project.Entities.SiteMap;
using WebApp.Common;
using WebApp.Data;

namespace WebApp.Filters
{
    public class AppAuthRequirement : IAuthorizationRequirement { }
    public class AppAuthHandler : AuthorizationHandler<AppAuthRequirement>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;

        public AppAuthHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthRequirement requirement)
        {
            var appContext = context.Resource as AuthorizationFilterContext;
            var routeData = appContext?.RouteData;
            var area = routeData?.Values["area"]?.ToString() ?? string.Empty;
            var controller = routeData?.Values["controller"]?.ToString();
            var action = routeData?.Values["action"]?.ToString();
            var method = appContext?.HttpContext?.Request?.Method;
            var userId = _userManager.GetUserId(context.User);
            if (appContext != null)
            {
#if DEBUG
                if (!_context.SiteMaps.Any(a =>
                 a.Area.Equals(area)
                 && a.Controller.Equals(controller)
                 && a.Action.Equals(action)
                 && a.Method.Equals(method)
                ) && _appSettings.DebugMode
                )
                {
                    var id = Guid.NewGuid().ToString();
                    var siteMap = new SiteMap
                    {
                        Id = id,
                        Area = area,
                        Controller = controller,
                        Action = action,
                        Method = method,
                        Title = $"/{(!string.IsNullOrEmpty(area) ? $"{area}/" : string.Empty)}{controller}/{action}",
                        Description =
                            $"/{(!string.IsNullOrEmpty(area) ? $"{area}/" : string.Empty)}{controller}/{action}",
                        ClassName = "btn",
                        ClassIcon = "fa fa-file",
                        IsActive = true,
                        SiteMapRoles = new List<SiteMapRole>
                        {
                            new SiteMapRole
                            {
                                RoleId = _appSettings.AdminRoleUid
                            }
                        }
                    };
                    _context.Add(siteMap);
                    _context.SaveChanges();
                }
#endif

            }
            var access = _context
                  .SiteMapRoles
                  .Include(i => i.SiteMap)
                  .Include(i => i.Role)
                  .ThenInclude(ti => ti.Users)
                   .Any(a =>
                   (a.Role.Users.Any(au => au.UserId == userId)
                   || a.SiteMap.IsPublic
                   )
                   && a.SiteMap.Area.Equals(area)
                   && a.SiteMap.Controller.Equals(controller)
                   && a.SiteMap.Action.Equals(action)
                   && a.SiteMap.Method.Equals(method)
                   && a.SiteMap.IsActive
                   );

            if (access)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
