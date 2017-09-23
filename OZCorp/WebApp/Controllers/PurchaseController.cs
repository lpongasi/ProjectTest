using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Project.Common.Common;
using Project.Common.Enums;
using Project.Entities.Identity;
using Project.Models.PurchaseOrder;
using WebApp.Common;
using System.Threading.Tasks;
using Project.Entities.JobOrder;
using System;
using Project.Common.Extensions;
using Project.Common.Filters;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Administrator,PurchaseItem,Franchisee,OfficeClerk")]
    [Authorize(Policy = Config.MainPolicy)]
    public class PurchaseController : CommonController<PurchaseController>
    {
        public PurchaseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(CommonFilter<string> gridFilter)
        {
            long.TryParse(gridFilter?.Filter?.ToLower().Replace("po", string.Empty), out long poId);
            var viewAll = User.IsInRole("Administrator") || User.IsInRole("OfficeClerk") || User.IsInRole("WarehouseUser");
            var result = Context.PurchaseOrder
               .Include(i => i.User)
               .Include(i => i.PurchaseOrderStatus)
               .Where(w => (viewAll || w.UserId.Equals(UserManager.GetUserId(User)))
                           &&
                           (string.IsNullOrEmpty(gridFilter.Filter)
                            || w.Id == poId
                            || w.User.CompanyAddress.Contains(gridFilter.Filter)
                            || w.User.CompanyContact.Contains(gridFilter.Filter)
                            || w.User.CompanyName.Contains(gridFilter.Filter)
                            || w.User.FirstName.Contains(gridFilter.Filter)
                            || w.User.LastName.Contains(gridFilter.Filter)
                            || w.PurchaseOrderStatus.Description.Contains(gridFilter.Filter)
                           ))
               .OrderByDescending(o => o.PoDate)
               .Select(s => new POViewModel
               {
                   Id = s.Id,
                   CompanyAddress = s.User.CompanyAddress,
                   CompanyContact = s.User.CompanyContact,
                   CompanyName = s.User.CompanyName,
                   FirstName = s.User.FirstName,
                   LastName = s.User.LastName,
                   Email = s.User.Email,
                   PoStatus = s.PurchaseOrderStatus.Description,
                   PoStatusId = s.PurchaseOrderStatusId,
                   PurchaseDate = s.PoDate
               })
               .ToResponse(gridFilter);

            return Json(result);
        }
        [HttpGet]
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult DetailPrint(long id)
        {
            var poDetail = GetPoDetail(id);
            var adminRoleId = Context.Roles.Where(w => w.Name.Equals("Administrator")).FirstOrDefault()?.Id;
            var userInfo = Context.Users.Include(i => i.Roles).Where(w => w.Roles.Any(a => a.RoleId.Equals(adminRoleId)))
                            .Select(s => new
                            {
                                s.CompanyAddress,
                                s.CompanyContact,
                                s.CompanyName
                            }).FirstOrDefault();
            poDetail.MainCompanyAddress = userInfo.CompanyAddress;
            poDetail.MainCompanyContact = userInfo.CompanyContact;
            poDetail.MainCompanyName = userInfo.CompanyName;
            return PartialView(poDetail);
        }
        public IActionResult InvDetailPrint(long id)
        {
            var poDetail = GetPoDetail(id);
            var adminRoleId = Context.Roles.Where(w => w.Name.Equals("Administrator")).FirstOrDefault()?.Id;
            var userInfo = Context.Users.Include(i => i.Roles).Where(w => w.Roles.Any(a => a.RoleId.Equals(adminRoleId)))
                            .Select(s => new
                            {
                                s.CompanyAddress,
                                s.CompanyContact,
                                s.CompanyName
                            }).FirstOrDefault();
            poDetail.MainCompanyAddress = userInfo.CompanyAddress;
            poDetail.MainCompanyContact = userInfo.CompanyContact;
            poDetail.MainCompanyName = userInfo.CompanyName;
            return PartialView(poDetail);
        }
        public IActionResult GetDetail(long id)
        {
            return Json(GetPoDetail(id));
        }
        [HttpPost]
        public IActionResult Update(long poid, decimal discount, decimal tax, decimal otherFees, string otherRemarks, string remarks)
        {
            var response = new Response();

            var po = Context.PurchaseOrder.FirstOrDefault(p => p.Id == poid);
            if (po != null && PoEditable(po.PurchaseOrderStatusId))
            {
                po.Discount = discount / 100;
                po.Tax = tax / 100;
                po.OtherFees = otherFees;
                po.OtherRemarks = otherRemarks;
                po.Remarks = remarks;
                Context.PurchaseOrder.Update(po);
                Context.SaveChanges();
                response.Success = true;
                return GetDetail(poid);
            }

            return Json(response);
        }
        [HttpPost]
        public IActionResult UpdateItem(long poid, long itemId, decimal discount, string remarks)
        {
            var response = new Response();

            var item = Context.PurchaseOrderItem.FirstOrDefault(a => a.ItemId == itemId && a.PurchaseOrderId == poid);
            var po = Context.PurchaseOrder.FirstOrDefault(a => a.Id == poid);
            if (item != null && PoEditable(po.PurchaseOrderStatusId))
            {
                item.Discount = discount / 100;
                item.Remarks = remarks;
                Context.PurchaseOrderItem.Update(item);
                Context.SaveChanges();
                response.Success = true;
                return GetDetail(poid);
            }
            return Json(response);
        }
        public async Task<IActionResult> Action(long id, PoStatus action)
        {
            var po = Context.PurchaseOrder
                .FirstOrDefault(a => a.Id == id);
            var response = new Response();

            if (po != null
                && ((po.PurchaseOrderStatusId == PoStatus.Purchase && (action == PoStatus.Approve || action == PoStatus.Declined))
               || (po.PurchaseOrderStatusId == PoStatus.Approve && action == PoStatus.Done)
               || (po.PurchaseOrderStatusId == PoStatus.Done && action == PoStatus.Paid)))
            {
                var items = Context.PurchaseOrderItem
                    .Include(i => i.Item)
                    .Where(w => w.PurchaseOrderId == id)
                    .ToList();
                var statusAction = string.Empty;
                var newJobOrder = new JobOrder();
                switch (action)
                {
                    case PoStatus.Declined:
                        items.ForEach(e =>
                        {
                            e.Item.Qty = e.Item.Qty + (e.Qty - e.JobQty);
                        });
                        po.PurchaseOrderStatusId = PoStatus.Declined;
                        Context.PurchaseOrder.Update(po);
                        Context.PurchaseOrderHistories.Add(new Project.Entities.PurchaseOrder.PurchaseOrderHistory
                        {
                            Action = "Declined",
                            ActionDate = DateTime.Now,
                            PoId = po.Id,
                            ActionUserId = UserManager.GetUserId(User)
                        });
                        Context.Item.UpdateRange(items.Select(s => s.Item));
                        await Context.SaveChangesAsync();
                        statusAction = "Declined";
                        response.Success = true;
                        break;
                    case PoStatus.Approve:
                        if (items.Sum(s => s.JobQty) > 0)
                        {
                            newJobOrder = new JobOrder
                            {
                                PurchaseOrderId = po.Id,
                                UserId = po.UserId,
                                JoDate = DateTime.Now,
                                JobOrderStatusId = JoStatus.CreatedWithPo,
                                JobOrderItems = items
                                        .Where(w => w.JobQty > 0).Select(s => new JobOrderItem
                                        {
                                            ItemId = s.ItemId,
                                            Qty = s.JobQty,
                                            StatusId = JoItemStatus.Pending
                                        }).ToList(),
                                History = new List<JobOrderHistory>
                                {
                                    new JobOrderHistory
                                    {
                                        ActionUserId =  UserManager.GetUserId(User),
                                        ActionDate = DateTime.Now,
                                        Action = "Approve = Created with Purchase order"
                                    }
                                }
                            };
                            Context.JobOrder.Add(newJobOrder);
                        }
                        po.PurchaseOrderStatusId = PoStatus.Approve;
                        Context.PurchaseOrder.Update(po);
                        Context.PurchaseOrderHistories.Add(new Project.Entities.PurchaseOrder.PurchaseOrderHistory
                        {
                            Action = "Approved",
                            ActionDate = DateTime.Now,
                            PoId = po.Id,
                            ActionUserId = UserManager.GetUserId(User)
                        });
                        await Context.SaveChangesAsync();
                        statusAction = "Approved";
                        response.Success = true;
                        break;
                    case PoStatus.Done:
                        if (items.Sum(s => s.JobQty) > 0)
                        {
                            var jobOrder = Context.JobOrder.FirstOrDefault(f => f.PurchaseOrderId == po.Id);
                            if (jobOrder != null && jobOrder.JobOrderStatusId == JoStatus.Done)
                            {
                                po.PurchaseOrderStatusId = PoStatus.Done;
                                Context.PurchaseOrder.Update(po);
                                Context.PurchaseOrderHistories.Add(new Project.Entities.PurchaseOrder.PurchaseOrderHistory
                                {
                                    Action = "Done",
                                    ActionDate = DateTime.Now,
                                    PoId = po.Id,
                                    ActionUserId = UserManager.GetUserId(User)
                                });
                                await Context.SaveChangesAsync();
                                response.Success = true;

                            }
                            else
                            {
                                response.Success = false;
                                response.Message = $"Job Order : JO{jobOrder.Id} is not yet Done!";
                            }
                            //Todo : Check JO is Done
                        }
                        else
                        {
                            po.PurchaseOrderStatusId = PoStatus.Done;
                            Context.PurchaseOrder.Update(po);
                            Context.PurchaseOrderHistories.Add(new Project.Entities.PurchaseOrder.PurchaseOrderHistory
                            {
                                Action = "Done",
                                ActionDate = DateTime.Now,
                                PoId = po.Id,
                                ActionUserId = UserManager.GetUserId(User)
                            });
                            await Context.SaveChangesAsync();
                            response.Success = true;
                        }
                        statusAction = "Done";
                        break;
                    case PoStatus.Paid:
                        po.PurchaseOrderStatusId = PoStatus.Paid;
                        Context.PurchaseOrder.Update(po);
                        Context.PurchaseOrderHistories.Add(new Project.Entities.PurchaseOrder.PurchaseOrderHistory
                        {
                            Action = "Paid",
                            ActionDate = DateTime.Now,
                            PoId = po.Id,
                            ActionUserId = UserManager.GetUserId(User)
                        });
                        await Context.SaveChangesAsync();
                        response.Success = true;

                        statusAction = "Mark as Paid";
                        break;

                }
                var urlPo = Url.Action("Detail", "Purchase", new { area = "", id = po.Id });
                Notify($"Purchase Order No: PO{po.Id} is {statusAction}", urlPo, $"PO{po.Id}", po.UserId);
                if (action == PoStatus.Approve && newJobOrder.Id > 0)
                {
                    var userIds = await GetUserIdsForRoles("Administrator", "ProductionSupervisor");
                    var urlJo = Url.Action("Detail", "JobOrder", new { area = "", id = newJobOrder.Id });
                    Notify($"New Job Order No: JO{newJobOrder.Id}", urlJo, Guid.NewGuid().ToString(), userIds);
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Invalid Actions!";
            }

            return Json(response);
        }
        public IEnumerable<POActionViewModel> PoValidActions(PoStatus status)
        {
            return
                Context.PurchaseOrderActions
                .Include(i => i.ValidStatus)
                .Where(w => w.PoStatusId == status)
                .Select(s =>
                new POActionViewModel
                {
                    Id = s.ValidStatus.Id,
                    Name = s.ValidStatus.Name,
                    Description = s.ValidStatus.Description,
                    ClassIcon = s.ValidStatus.ClassIcon,
                    ClassName = s.ValidStatus.ClassName
                })
                .ToList();
        }
        public POViewModel GetPoDetail(long id)
        {
            var result = Context.PurchaseOrder
                 .Include(i => i.PurchaseOrderItems)
                 .ThenInclude(ti => ti.Item)
                 .ThenInclude(ti => ti.Size)
                 .Include(i => i.User)
                 .Include(i => i.PurchaseOrderStatus)
                 .Where(w => w.Id == id)
                 .Select(s => new POViewModel
                 {
                     Id = s.Id,
                     UserId = s.UserId,
                     CompanyAddress = s.User.CompanyAddress,
                     CompanyContact = s.User.CompanyContact,
                     CompanyName = s.User.CompanyName,
                     Discount = s.Discount,
                     Email = s.User.Email,
                     FirstName = s.User.FirstName,
                     LastName = s.User.LastName,
                     OtherFees = s.OtherFees,
                     OtherRemarks = s.OtherRemarks,
                     Remarks = s.Remarks,
                     Tax = s.Tax,
                     PoStatus = s.PurchaseOrderStatus.Description,
                     PoStatusId = s.PurchaseOrderStatusId,
                     PurchaseDate = s.PoDate,
                     Items = s.PurchaseOrderItems.Select(si => new POITemViewModel
                     {
                         PurchaseOrderId = si.PurchaseOrderId,
                         ItemId = si.ItemId,
                         Barcode = si.Item.Barcode,
                         Discount = si.Discount,
                         Price = si.Price,
                         Qty = si.Qty,
                         Remarks = si.Remarks,
                         Name = si.Item.Name,
                         Size = si.Item.Size != null ? si.Item.Size.Name : null,
                         Description = si.Item.Description
                     }),
                 })
                 .FirstOrDefault();
            if (result != null)
            {
                result.Editable = PoEditable(result.PoStatusId);
                result.Actions = (User.IsInRole("Administrator") || User.IsInRole("OfficeClerk")) ? PoValidActions(result.PoStatusId)
                     : new List<POActionViewModel>();
            }
            result.Printable = result.PoStatusId != PoStatus.Declined && (User.IsInRole("Administrator") || User.IsInRole("OfficeClerk"));

            return result;
        }

    }

}