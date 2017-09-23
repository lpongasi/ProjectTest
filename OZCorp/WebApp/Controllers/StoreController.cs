using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Project.Common.Common;
using Project.Common.Extensions;
using Project.Common.Filters;
using Project.Entities.Cart;
using Project.Entities.Identity;
using WebApp.Common;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Administrator,PurchaseItem,Franchisee")]
    [Authorize(Policy = Config.MainPolicy)]
    public class StoreController : CommonController<StoreController>
    {
        public StoreController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(StoreFilter gridFilter)

        {
            var myCart = Context.MyCart.Where(w => w.UserId == UserManager.GetUserId(User)).Select(s => s.ItemId).ToList() ?? new List<long>();
            var isAdmin = User.IsInRole("Administrator");
            var items = Context.Item
                .Include(i => i.Size)
                .Include(i => i.Category)
                .Include(i => i.SubCategory)
                .Include(i => i.UnitOfMeasure)
                .Include(i => i.Images)
                .Where(w =>
                  !w.Removed && !w.NotForSale
                && (!w.IsPrivate || isAdmin)
                && (w.CategoryId == gridFilter.Category || gridFilter.Category == 0)
                && (w.SubCategoryId == gridFilter.SubCategory || gridFilter.SubCategory == 0)
                &&
                (
                string.IsNullOrEmpty(gridFilter.Filter)
                || w.Barcode.Contains(gridFilter.Filter)
                || w.Name.Contains(gridFilter.Filter)
                || w.Description.Contains(gridFilter.Filter)
                || w.StockNo.Contains(gridFilter.Filter)
                || w.PartNo.Contains(gridFilter.Filter)
                || w.UnitOfMeasure.Name.Contains(gridFilter.Filter)
                )
                )
                .Select(s => new
                {
                    s.Id,
                    s.Barcode,
                    s.Name,
                    s.Description,
                    s.Price,
                    priceString = s.Price.ToString("N2"),
                    s.Qty,
                    size = s.Size.Name,
                    uom = s.UnitOfMeasure.Name,
                    category = s.Category.Name,
                    s.Images,
                    IsAdded = myCart.Contains(s.Id),
                    s.PurPro
                })
                .OrderBy(o => o.Name)
                .ToResponse(gridFilter);

            return Json(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(long itemId, int quantity)
        {
            var response = new Response();

            if (!ItemAvailable(itemId, quantity))
            {
                response.Success = false;
                response.Message = "Item Not Available/Quantity is too high from quantity left!";
            }
            else if (Context.MyCart.Any(a => a.ItemId == itemId && a.UserId.Equals(UserManager.GetUserId(User))))
            {

                response.Success = false;
                response.Message = "Item Already Added!";
            }
            else
            {
                Context.MyCart.Add(new MyCart
                {
                    ItemId = itemId,
                    Quantity = quantity,
                    Selected = true,
                    UserId = UserManager.GetUserId(User)
                });
                await Context.SaveChangesAsync();
                response.Success = true;
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(long id)
        {
            var response = new Response();
            var cartItem = Context.MyCart.FirstOrDefault(a => a.ItemId == id && a.UserId.Equals(UserManager.GetUserId(User)));
            if (cartItem != null)
            {
                Context.MyCart.Remove(cartItem);
                await Context.SaveChangesAsync();
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "Item not found in cart!";
            }
            return Json(response);
        }
    }
}