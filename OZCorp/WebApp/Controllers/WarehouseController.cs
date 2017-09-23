using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Common.Extensions;
using Project.Common.Filters;
using Project.Entities.Identity;
using Project.Models.Item;
using WebApp.Common;
using WebApp.Data;

namespace WebApp.Controllers
{
    [Authorize(Policy = Config.MainPolicy)]
    public class WarehouseController : CommonController<WarehouseController>
    {
        public WarehouseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(CommonFilter<string> gridFilter)

        {
            var items = Context.Item
                .Include(i => i.Size)
                .Include(i => i.Category)
                .Include(i => i.SubCategory)
                .Include(i => i.UnitOfMeasure)
                .Include(i => i.Images)
                .Where(w => (string.IsNullOrEmpty(gridFilter.Filter)
                             || w.Name.Contains(gridFilter.Filter)
                             || w.Description.Contains(gridFilter.Filter)
                             || w.StockNo.Contains(gridFilter.Filter)
                             || w.PartNo.Contains(gridFilter.Filter)
                             || w.Category.Name.Contains(gridFilter.Filter)
                             || w.SubCategory.Name.Contains(gridFilter.Filter)
                             || w.UnitOfMeasure.Name.Contains(gridFilter.Filter))
                            && !w.Removed
                )
                .Select(s => new ItemViewModel
                {
                    Id = s.Id,
                    Barcode = s.Barcode,
                    Name = s.Name,
                    Description = s.Description,
                    UnitCost = s.UnitCost,
                    Price = s.Price,
                    Qty = s.Qty,
                    StockNo = s.StockNo,
                    PartNo = s.PartNo,
                    Location = s.Location,
                    Size = s.Size.Name,
                    Uom = s.UnitOfMeasure.Name,
                    Category = s.Category.Name,
                    SubCategory = s.SubCategory.Name,
                    Images = s.Images
                })
                .OrderBy(o => o.Name)
                .ToResponse(gridFilter);
            return Json(items);
        }

    }
}