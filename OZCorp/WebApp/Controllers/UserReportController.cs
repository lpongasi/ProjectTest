using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Common.Common;
using Project.Common.Extensions;
using Project.Common.Filters;
using Project.Entities.Identity;
using Project.Entities.UserReports;
using Project.Models.Report;
using WebApp.Common;
using WebApp.Data;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Policy = Config.MainPolicy)]
    public class UserReportController : CommonController<UserReportController>
    {
        public UserReportController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View("Index");
        }
        public IActionResult Detail()
        {
            return View("Index");
        }
        public IActionResult ReportDetailPrint(long id)
        {
            return View(Detailed(id));
        }
        public IActionResult ReportDetail(long id)
        {
            return Json(Detailed(id));
        }
        public ReportedList Detailed(long id)
        {

            var isAdmin = User.IsInRole("Administrator");
            var report = Context.UserReport
                .Include(i => i.User)
                .Include(i => i.UserReportDetail)
                .ThenInclude(i => i.ReportDetail)
                .Where(w => w.Id == id)
                .Select(s => new ReportedList
                {
                    Id = s.Id,
                    Email = s.User.Email,
                    CompanyAddress = s.User.CompanyAddress,
                    CompanyContact = s.User.CompanyContact,
                    CompanyName = s.User.CompanyName,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    IsLock = s.IsLock,
                    Year = s.Year,
                    Month = s.Month,
                    IsAdmin = isAdmin,
                    List = s.UserReportDetail.Select(us => new ReportedViewList
                    {
                        Title = us.ReportDetail.Name,
                        Name = us.ReportDetail.Id,
                        Id = us.ReportDetail.Id,
                        Value = us.Value,
                        Description = us.ReportDetail.Description,
                        Sequence = us.ReportDetail.Sequence,
                        IsField = us.ReportDetail.IsField,
                        ParentId = us.ReportDetail.ParentId
                    }).ToList()
                }).FirstOrDefault();
            var repIds = report.List.Select(s => s.Id);
            var replist = Context.ReportDetail.Where(w => !repIds.Contains(w.Id)).Select(us => new ReportedViewList
            {
                Title = us.Name,
                Name = us.Id,
                Id = us.Id,
                Value = 0,
                Description = us.Description,
                Sequence = us.Sequence,
                IsField = us.IsField,
                ParentId = us.ParentId
            }).ToList();
            replist.AddRange(report.List);
            report.List = replist.OrderBy(s => s.Sequence).ToList();
            return report;
        }
        public IActionResult Reports(CommonFilter<string> gridFilter)
        {
            var isAdmin = User.IsInRole("Administrator");
            var userId = UserManager.GetUserId(User);
            var reports = Context.UserReport
                .Include(i => i.User)
                .Where(u =>
                ((isAdmin && u.IsLock) || u.UserId.Equals(userId))
                && (
                    string.IsNullOrEmpty(gridFilter.Filter)
                    || u.User.Email.Contains(gridFilter.Filter)
                    || u.User.CompanyAddress.Contains(gridFilter.Filter)
                    || u.User.CompanyContact.Contains(gridFilter.Filter)
                    || u.User.CompanyName.Contains(gridFilter.Filter)
                    || u.User.FirstName.Contains(gridFilter.Filter)
                    || u.User.LastName.Contains(gridFilter.Filter)
                )
                )
                .Select(s => new ReportedList
                {
                    Id = s.Id,
                    Email = s.User.Email,
                    CompanyAddress = s.User.CompanyAddress,
                    CompanyContact = s.User.CompanyContact,
                    CompanyName = s.User.CompanyName,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    IsLock = s.IsLock,
                    Year = s.Year,
                    Month = s.Month
                })
                .OrderByDescending(o => o.Year)
                .ThenByDescending(o => o.Month)
                .ToResponse(gridFilter);

            return Json(reports);
        }
        public IActionResult List()
        {
            return Json(Context.ReportDetail.OrderBy(o => o.Sequence).ToList());
        }


        public async Task<IActionResult> Report(ReportedView reportedView)
        {
            var response = new Response();
            if (reportedView != null &&
                reportedView.Month >= 1 && reportedView.Month <= 12
                && reportedView.Year > 1950
                &&
                !Context.UserReport.Any(a =>
                    a.Month == reportedView.Month
                    && a.Year == reportedView.Year
                    && a.UserId.Equals(UserManager.GetUserId(User))
                ))
            {
                var userReport = new UserReport
                {
                    UserId = UserManager.GetUserId(User),
                    Month = reportedView.Month,
                    Year = reportedView.Year,
                    IsLock = reportedView.Action.Equals("lock"),
                    UserReportDetail = reportedView.Data.Select(s => new UserReportDetail
                    {
                        RdId = s.Name,
                        Value = s.Value
                    })
                        .ToList()
                };
                Context.Add(userReport);
                Context.SaveChanges();
                if (reportedView.Action.Equals("lock"))
                {
                    var userIds = await GetUserIdsForRoles("Administrator");
                    Notify("New Monthly Report",
                        Url.Action("Detail", "UserReport", new { area = "", id = userReport.Id }), $"REP{userReport.Id}",
                        userIds);
                }
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Report Date already Exists!";
            }
            return Json(response);
        }
        public async Task<IActionResult> ReportUpdate(ReportedViewUpdate reportedView)
        {
            var response = new Response();
            var isAdmin = User.IsInRole("Administrator");
            var userId = UserManager.GetUserId(User);
            var report = Context.UserReport.Include(i => i.UserReportDetail).FirstOrDefault(a => a.Id == reportedView.Id && (a.UserId.Equals(userId) || isAdmin));
            if (reportedView != null && report != null)
            {
                report.IsLock = reportedView.Action.Equals("lock");
                foreach (var userReportDetail in report.UserReportDetail)
                {
                    userReportDetail.Value = reportedView.Data.First(w => w.Name == userReportDetail.RdId)
                        .Value;
                }
                Context.Update(report);
                Context.SaveChanges();
                if (reportedView.Action.Equals("lock"))
                {
                    var userIds = await GetUserIdsForRoles("Administrator");
                    Notify("New Monthly Report",
                        Url.Action("Detail", "UserReport", new { area = "", id = reportedView.Id }), $"REP{reportedView.Id}",
                        userIds);
                }
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Item Not Available!";
            }
            return Json(response);
        }
        public IActionResult Unlock(long id)
        {
            var response = new Response();
            var isAdmin = User.IsInRole("Administrator");
            var report = Context.UserReport.FirstOrDefault(a => a.Id == id && isAdmin);
            if (report != null)
            {
                report.IsLock = false;
                Context.UserReport.Update(report);
                Context.SaveChanges();
                response.Success = true;
                Notify($"Monthly Report Unlock",
                    Url.Action("Detail", "UserReport", new { area = "", id }), $"REP{id}",
                    report.UserId);
            }
            else
            {
                response.Success = false;
                response.Message = "Item Not Available!";
            }
            return Json(response);
        }
    }
}