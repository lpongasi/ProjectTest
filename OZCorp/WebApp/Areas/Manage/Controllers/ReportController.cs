using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Common.Filters;
using Project.Entities.Identity;
using WebApp.Common;
using WebApp.Controllers;
using WebApp.Data;
using Dapper;
using Project.Common;
using Project.Common.Common;
using Project.Models.Report;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("Manage"), Authorize(Policy = Config.MainPolicy)]
    public class ReportController : CommonController<ItemController>
    {
        public ReportController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(CommonFilter<string> commonFilter, DateTime? dateFrom, DateTime? dateTo, bool limitOffset = true, int? categoryId = null, int? subCategoryId = null)
        {
            var response = new ResponseFilter<ItemReportViewResponse>();
            var today = DateTime.Now;
            dateFrom = dateFrom ?? new DateTime(today.Year, today.Month, 1);
            dateTo = dateTo ?? new DateTime(today.Year, today.Month + 1, 1).AddDays(-1);
            commonFilter.Filter = string.IsNullOrEmpty(commonFilter.Filter) ? null : $"%{commonFilter.Filter}%";

            using (var connection = Context.Database.GetDbConnection())
            {
                response.Total = connection.Query<int?>(SqlReportItem.SelectCount, new
                {
                    Search = commonFilter.Filter,
                    From = dateFrom,
                    To = dateTo,
                    CategoryId = categoryId,
                    SubCategoryId = subCategoryId
                }).FirstOrDefault() ?? 0;

                response.Page = commonFilter.Page;
                response.PageSize = commonFilter.PageSize;

                response.Data = new ItemReportViewResponse
                {
                    ItemReportViews = connection.Query<ItemReportView>(SqlReportItem.SelectList(limitOffset), new
                    {
                        Search = commonFilter.Filter,
                        From = dateFrom,
                        To = dateTo,
                        Limit = commonFilter.PageSize,
                        Offset = (commonFilter.Page - 1) * commonFilter.PageSize,
                        CategoryId = categoryId,
                        SubCategoryId = subCategoryId
                    }).ToList(),

                    ItemReportViewTotal = connection.Query<ItemReportViewTotal>(SqlReportItem.SelectTotal, new
                    {
                        Search = commonFilter.Filter,
                        From = dateFrom,
                        To = dateTo,
                        CategoryId = categoryId,
                        SubCategoryId = subCategoryId
                    }).FirstOrDefault()
                };
                if (response.Data.ItemReportViews.Any())
                {
                    var images = connection.Query<KeyValuePair<long,string>>(SqlItem.ImageList(response.Data.ItemReportViews
                        .Select(s => s.Id).ToList())).ToList();
                    foreach (var item in response.Data.ItemReportViews)
                    {
                        item.ImageLocation =
                            images.Where(w => w.Key == item.Id).Select(s => s.Value).FirstOrDefault() ??
                            Constants.NoImage;
                    }
                }
            }
            return Json(response);
        }
        public IActionResult Company()
        {
            return View();
        }

        public IActionResult CompanyList(CommonFilter<string> commonFilter, DateTime? dateFrom, DateTime? dateTo, bool limitOffset = true)
        {
            var response = new ResponseFilter<CompanyReportViewResponse>();
            var today = DateTime.Now;
            dateFrom = dateFrom ?? new DateTime(today.Year, today.Month, 1);
            dateTo = dateTo ?? new DateTime(today.Year, today.Month + 1, 1).AddDays(-1);
            commonFilter.Filter = string.IsNullOrEmpty(commonFilter.Filter) ? null : $"%{commonFilter.Filter}%";
            response.Page = commonFilter.Page;
            response.PageSize = commonFilter.PageSize;

            using (var connection = Context.Database.GetDbConnection())
            {
                response.Total = connection.Query<int?>(SqlReportCompany.SelectCount, new
                {
                    Search = commonFilter.Filter,
                    From = dateFrom,
                    To = dateTo
                }).FirstOrDefault() ?? 0;

                response.Data = new CompanyReportViewResponse
                {
                    CompanyReportViews = connection.Query<CompanyReportView>(SqlReportCompany.SelectList(limitOffset),
                        new
                        {
                            From = dateFrom,
                            To = dateTo,
                            Search = commonFilter.Filter,
                            Limit = commonFilter.PageSize,
                            Offset = (commonFilter.Page - 1) * commonFilter.PageSize
                        }).ToList(),
                    CompanyReportViewTotal = connection.Query<CompanyReportViewTotal>(SqlReportCompany.SelectTotal,
                        new
                        {
                            From = dateFrom,
                            To = dateTo,
                            Search = commonFilter.Filter
                        }).FirstOrDefault()
                };
            }
            return Json(response);
        }
    }
}