using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Project.Common.Common;
using Project.Common.Enums;
using Project.Common.Extensions;
using Project.Entities.Identity;
using Project.Entities.PurchaseOrder;
using WebApp.Common;
using Project.Models.Cart;
using Project.Common.Filters;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Administrator,PurchaseItem,Franchisee")]
    [Authorize(Policy = Config.MainPolicy)]
    public class MyCartController : CommonController<MyCartController>
    {
        public MyCartController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }

        public IActionResult Index()
        {
            ViewData["Total"] = Context.MyCart
                  .Include(i => i.Item)
                   .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected)
                  .Sum(s => s.Quantity * s.Item.Price);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> List(CommonFilter<string> gridFilter)
        => Json(await Context.MyCart
                 .Include(i => i.Item)
                 .Include(i => i.Item.Images)
                 .Include(i => i.Item.UnitOfMeasure)
                 .Where(w =>
                           w.UserId.Equals(UserManager.GetUserId(User))
                        && (string.IsNullOrWhiteSpace(gridFilter.Filter) || w.Item.Name.Contains(gridFilter.Filter) || w.Item.Description.Contains(gridFilter.Filter))
                 )
                 .Select(s => new MyCartViewModel
                 {
                     ItemId = s.ItemId,
                     Quantity = s.Quantity,
                     Barcode = s.Item.Barcode,
                     Name = s.Item.Name,
                     Description = s.Item.Description,
                     ItemNotForSale = s.Item.NotForSale,
                     Price = s.Item.Price,
                     QuantityLeft = s.Item.Qty,
                     ItemRemoved = s.Item.Removed,
                     PurPro = s.Item.PurPro,
                     Selected = s.Selected,
                     UnitOfMeasure = s.Item.UnitOfMeasure.Name,
                     ImageLocation = s.Item.Images.Select(si => si.FileLocation)
                 }
                 ).ToResponseAsync(gridFilter));
        public async Task<IActionResult> PurchaseList()
        => Json(await Context.MyCart
                 .Include(i => i.Item)
                 .ThenInclude(i => i.UnitOfMeasure)
                 .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected
                        && !w.Item.NotForSale && !w.Item.Removed && (w.Item.Qty >= w.Quantity || w.Item.PurPro)
                 )
                 .Select(s => new MyCartViewModel
                 {
                     ItemId = s.ItemId,
                     Quantity = s.Quantity,
                     Barcode = s.Item.Barcode,
                     Name = s.Item.Name,
                     Description = s.Item.Description,
                     ItemNotForSale = s.Item.NotForSale,
                     Price = s.Item.Price,
                     QuantityLeft = s.Item.Qty,
                     ItemRemoved = s.Item.Removed,
                     PurPro = s.Item.PurPro,
                     Selected = s.Selected,
                     UnitOfMeasure = s.Item.UnitOfMeasure.Name
                 }).ToResponseAsync());
        public async Task<IActionResult> ItemSelect(string id, bool action)
        {
            var result = new Response<string>();
            try
            {
                var ids = id.Split(',').Select(long.Parse).ToList();
                if ((ids?.Count ?? 0) > 0)
                {
                    var carts = Context
                        .MyCart
                        .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && ids.Contains(w.ItemId))
                        .ToList();
                    carts.ForEach(e =>
                    {
                        e.Selected = action;
                    });
                    Context.MyCart.UpdateRange(carts);
                    await Context.SaveChangesAsync();

                    result.Data = Context.MyCart
                     .Include(i => i.Item)
                     .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected)
                     .Sum(s => s.Quantity * s.Item.Price)
                     .ToString("N2");
                }
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = "Action failed!" + e.Message;
            }


            return Json(result);
        }
        public async Task<IActionResult> UpdateItem(long itemId, int quantity)
        {
            var response = new Response<string>();
            var cartItem = Context.MyCart.FirstOrDefault(a => a.ItemId == itemId && a.UserId.Equals(UserManager.GetUserId(User)));
            if (!ItemAvailable(itemId, quantity))
            {
                response.Success = false;
                response.Message = "Item Not Available/Quantity is too high from quantity left!";
            }
            else if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                Context.MyCart.Update(cartItem);
                await Context.SaveChangesAsync();
                response.Success = true;
                response.Data = Context.MyCart
                     .Include(i => i.Item)
                     .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected)
                     .Sum(s => s.Quantity * s.Item.Price)
                     .ToString("N2");
            }
            return Json(response);
        }
        public async Task<IActionResult> RemoveItem(long itemId)
        {
            var response = new Response<string>();
            var cartItem = Context.MyCart.FirstOrDefault(a => a.ItemId == itemId && a.UserId.Equals(UserManager.GetUserId(User)));
            if (cartItem != null)
            {
                Context.MyCart.Remove(cartItem);
                await Context.SaveChangesAsync();
                response.Success = true;
                response.Data = Context.MyCart
                     .Include(i => i.Item)
                     .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected)
                     .Sum(s => s.Quantity * s.Item.Price)
                     .ToString("N2");
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Purchase(string action)
        {
            var response = new Response<string>();
            var cartCount = Context.MyCart.Count(w => w.UserId.Equals(UserManager.GetUserId(User)));
            var selectedCount =
                cartCount <= 0 ? 0
                : Context.MyCart.Include(i => i.Item).Count(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected
                              && !w.Item.NotForSale && !w.Item.Removed && (w.Item.Qty >= w.Quantity || w.Item.PurPro));
            ViewData["Total"] =
                cartCount <= 0 ? 0
                : Context.MyCart
                 .Include(i => i.Item)
                  .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected
                  && !w.Item.NotForSale && !w.Item.Removed && (w.Item.Qty >= w.Quantity || w.Item.PurPro))
                 .Sum(s => s.Quantity * s.Item.Price);
            if (cartCount <= 0)
            {
                response.Success = false;
                response.Message = "No Item in Cart";
            }
            else if (selectedCount <= 0 &&
                (action.Equals("purchase")
                || action.Equals("preserve"))

                )
            {
                response.Success = false;
                response.Message = "No Selected Item found";
            }
            else
            {
                using (var transaction = await Context.Database.BeginTransactionAsync())
                {
                    //try
                    //{
                    if (action.Equals("clear"))
                    {
                        Context.MyCart.RemoveRange(Context.MyCart.Where(w => w.UserId.Equals(UserManager.GetUserId(User))));
                        await Context.SaveChangesAsync();
                        transaction.Commit();
                        response.Data = Url.Action("Index", "MyCart", new { area = "" });
                    }
                    else
                    {
                        var selectedItem = Context.MyCart.Include(i => i.Item)
                            .Where(w => w.UserId.Equals(UserManager.GetUserId(User)) && w.Selected
                            && !w.Item.NotForSale && !w.Item.Removed && (w.Item.Qty >= w.Quantity || w.Item.PurPro))
                            .ToList();
                        var currentUser = await UserManager.GetUserAsync(User);

                        var purchase = new PurchaseOrder
                        {
                            UserId = currentUser.Id,
                            Discount = currentUser.Discount,
                            Tax = currentUser.Tax,
                            OtherFees = currentUser.OtherFees,
                            OtherRemarks = currentUser.OtherRemarks,
                            PoDate = DateTime.Now,
                            PurchaseOrderStatusId = PoStatus.Purchase,
                            PurchaseOrderItems = new List<PurchaseOrderItem>()
                        };
                        selectedItem.ForEach(e =>
                        {
                            purchase.PurchaseOrderItems.Add(new PurchaseOrderItem
                            {
                                ItemId = e.ItemId,
                                Qty = e.Quantity,
                                JobQty = e.Quantity <= e.Item.Qty
                                              ? 0 : e.Quantity - e.Item.Qty,
                                Price = e.Item.Price,
                                UnitCost = e.Item.UnitCost
                            });
                            e.Item.Qty = e.Quantity >= e.Item.Qty ? 0 : e.Item.Qty - e.Quantity;
                        });
                        purchase.History = new List<PurchaseOrderHistory>
                            {
                                new PurchaseOrderHistory
                                {
                                    Action = "Create PO",
                                    ActionDate = DateTime.Now,
                                    ActionUserId = UserManager.GetUserId(User)
                                }
                            };

                        Context.PurchaseOrder.Add(purchase);
                        Context.Item.UpdateRange(selectedItem.Select(s => s.Item));


                        if (action.Equals("purchase"))
                        {
                            Context.MyCart.RemoveRange(selectedItem);
                        }
                        await Context.SaveChangesAsync();
                        transaction.Commit();
                        var url = Url.Action("Detail", "Purchase", new { area = "", id = purchase.Id });
                        response.Data = url;

                        var userIds = await GetUserIdsForRoles("Administrator","OfficeClerk");
                        Notify($"New Purchase Order PO{purchase.Id}", url, Guid.NewGuid().ToString() , userIds);
                    }
                    response.Success = true;
                    response.Message = "Item Purchase";
                    //}
                    //catch (Exception)
                    //{
                    //    transaction.Rollback();
                    //    response.Data = string.Empty;
                    //    response.Success = false;
                    //    response.Message = "Server Problems found!";
                    //}
                }
            }
            return Json(response);
        }
    }
}