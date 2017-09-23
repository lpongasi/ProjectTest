using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Project.Entities.Identity;
using WebApp.Common;
using Project.Common.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Project.Common.Common;
using Project.Common.Enums;
using Project.Models.JobOrder;
using Project.Common.Extensions;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Administrator,ProductionSupervisor,WarehouseClerk")]
    [Authorize(Policy = Config.MainPolicy)]
    public class JobOrderController : CommonController<JobOrderController>
    {
        public JobOrderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Detail()
        {
            return View("Index");
        }
        public IActionResult List(CommonFilter<string> gridFilter)
        {
            long.TryParse(gridFilter?.Filter?.ToLower().Replace("po", string.Empty), out long poId);
            long.TryParse(gridFilter?.Filter?.ToLower().Replace("jo", string.Empty), out long joid);
            var viewAll = User.IsInRole("Administrator") || User.IsInRole("OfficeClerk") || User.IsInRole("WarehouseUser");
            var result = Context.JobOrder
               .Include(i => i.User)
               .Include(i => i.JobOrderStatus)
               .Where(w => (viewAll || w.UserId.Equals(UserManager.GetUserId(User)))
                           &&
                           (string.IsNullOrEmpty(gridFilter.Filter)
                            || w.Id == joid
                            || w.PurchaseOrderId == poId
                            || w.User.CompanyAddress.Contains(gridFilter.Filter)
                            || w.User.CompanyContact.Contains(gridFilter.Filter)
                            || w.User.CompanyName.Contains(gridFilter.Filter)
                            || w.User.FirstName.Contains(gridFilter.Filter)
                            || w.User.LastName.Contains(gridFilter.Filter)
                            || w.JobOrderStatus.Description.Contains(gridFilter.Filter)
                           ))
               .OrderByDescending(o => o.JoDate)
               .Select(s => new JOViewModel
               {
                   Id = s.Id,
                   PoId = s.PurchaseOrderId,
                   CompanyAddress = s.User.CompanyAddress,
                   CompanyContact = s.User.CompanyContact,
                   CompanyName = s.User.CompanyName,
                   FirstName = s.User.FirstName,
                   LastName = s.User.LastName,
                   Email = s.User.Email,
                   JoStatus = s.JobOrderStatus.Description,
                   JoStatusId = s.JobOrderStatusId,
                   JoDate = s.JoDate
               })
               .ToResponse(gridFilter);

            return Json(result);
        }
        [HttpPost]
        public IActionResult Detail(long id)
        {
            return Json(JoDetail(id));
        }

        [HttpPost]
        public IActionResult ItemSet(long id, long itemId,int? day,int? hour,int? minute)
        {
            var item = Context.JobOrderItem.FirstOrDefault(f => f.JobOrderId == id && f.ItemId == itemId);
            if (item.StatusId != JoItemStatus.Pending)
                return Json(new Response
                {
                    Success = false
                });
            item.EstDay = day ?? 0;
            item.EstHour = hour ?? 0;
            item.EstMinute = minute ?? 0;
            Context.JobOrderItem.Update(item);
            Context.SaveChanges();
            return Json(new Response
            {
                Success = true
            });
        }
        [HttpPost]
        public async Task<IActionResult> ItemAction(long id, long itemId, JoItemStatus actionId)
        {
            var response = new Response<JOViewModel>();
            var item = Context.JobOrderItem.FirstOrDefault(f => f.JobOrderId == id && f.ItemId == itemId);
            if (item.EstDay + item.EstHour + item.EstMinute <= 0)
            {
                response.Success = false;
                response.Message = "Set Estimated Time First!";
                return Json(response);
            }

            var started = false;
            var jo = Context.JobOrder.FirstOrDefault(f => f.Id == id);
            if (Context.JobOrderItemAction.Any(a => a.StatusId == item.StatusId && a.ValidStatusId == actionId))
            {
                if (actionId == JoItemStatus.Started)
                {
                    item.DateTimeStart = DateTime.Now;
                    item.StatusId = JoItemStatus.Started;
                    if (jo.JobOrderStatusId == JoStatus.Created || jo.JobOrderStatusId == JoStatus.CreatedWithPo)
                    {
                        jo.JobOrderStatusId = JoStatus.Started;
                        Context.JobOrder.Update(jo);
                        started = true;
                    }
                }
                else
                {
                    item.DateTimeEnd = DateTime.Now;
                    item.StatusId = JoItemStatus.Done;
                }
                Context.JobOrderItem.Update(item);
                await Context.SaveChangesAsync();

                if (started)
                {
                    var userIds = await GetUserIdsForRoles("Administrator", "OfficeClerk");
                    var urlJo = Url.Action("Detail", "JobOrder", new { area = "", id = jo.Id });
                    Notify($"Job Order No: JO{jo.Id} is Started!", urlJo, Guid.NewGuid().ToString(), userIds);
                }
                
                if (jo.JobOrderStatusId == JoStatus.Started && actionId == JoItemStatus.Done &&
                    Context.JobOrderItem.Count(c => c.JobOrderId == id && c.StatusId == JoItemStatus.Done) == Context.JobOrderItem.Count(c => c.JobOrderId == id))
                {
                    var po = Context.PurchaseOrder.First(f => f.Id == jo.PurchaseOrderId);
                    po.PurchaseOrderStatusId = PoStatus.Done;
                    jo.JobOrderStatusId = JoStatus.Done;
                    Context.JobOrder.Update(jo);
                    Context.PurchaseOrder.Update(po);
                    await Context.SaveChangesAsync();

                    var userIds = await GetUserIdsForRoles("Administrator", "OfficeClerk");
                    var urlJo = Url.Action("Detail", "JobOrder", new { area = "", id = jo.Id });
                    Notify($"Job Order No: JO{jo.Id} is DONE!", urlJo, Guid.NewGuid().ToString(), userIds);
                }
            }
            response.Success = true;
            response.Data = JoDetail(id);
            return Json(response);
        }

        public IActionResult DetailPrint(long id)
        {
            var detail = JoDetail(id);
            var adminRoleId = Context.Roles.FirstOrDefault(w => w.Name.Equals("Administrator"))?.Id;
            var userInfo = Context.Users.Include(i => i.Roles).Where(w => w.Roles.Any(a => a.RoleId.Equals(adminRoleId)))
                .Select(s => new
                {
                    s.CompanyAddress,
                    s.CompanyContact,
                    s.CompanyName
                }).FirstOrDefault();
            detail.MainCompanyAddress = userInfo.CompanyAddress;
            detail.MainCompanyContact = userInfo.CompanyContact;
            detail.MainCompanyName = userInfo.CompanyName;
            return View(detail);
        }
        public JOViewModel JoDetail(long id)
        {

            var result = Context.JobOrder
                .Include(i => i.JobOrderItems)
                .ThenInclude(ti => ti.Item)
                .ThenInclude(ti=>ti.Size)
                .Include(i => i.JobOrderItems)
                .ThenInclude(ti => ti.Status)
                .Include(i => i.User)
                .Include(i => i.JobOrderStatus)
                .Where(w => w.Id == id)
                .Select(s => new JOViewModel
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    CompanyAddress = s.User.CompanyAddress,
                    CompanyContact = s.User.CompanyContact,
                    CompanyName = s.User.CompanyName,
                    Email = s.User.Email,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    JoStatus = s.JobOrderStatus.Description,
                    JoStatusId = s.JobOrderStatusId,
                    JoDate = s.JoDate,
                    Items = s.JobOrderItems.Select(si => new JOITemViewModel
                    {
                        JobOrderId = si.JobOrderId,
                        ItemId = si.ItemId,
                        Barcode = si.Item.Barcode,
                        Qty = si.Qty,
                        Name = si.Item.Name,
                        Size = si.Item.Size!=null? si.Item.Size.Name:null,
                        Description = si.Item.Description,
                        Status = si.Status.Description,
                        StatusId = si.StatusId,
                        DateTimeStart = si.DateTimeStart,
                        DateTimeEnd = si.DateTimeEnd,
                        EstDay = si.EstDay,
                        EstHour = si.EstHour,
                        EstMinute = si.EstMinute
                    })
                })
                .FirstOrDefault();
            if (result != null)
            {
                var statusList = result.Items.Select(s => s.StatusId).Distinct().ToList();
                var actionList = Context.JobOrderItemAction
                    .Include(i => i.ValidStatus)
                    .Where(w => statusList.Contains(w.StatusId))
                    .Select(s => new JOItemActionViewModel()
                    {
                        CurrentStatus = s.StatusId,
                        Id = s.ValidStatusId,
                        Name = s.ValidStatus.Name,
                        Description = s.ValidStatus.Description,
                        ClassIcon = s.ValidStatus.ClassIcon,
                        ClassName = s.ValidStatus.ClassName
                    })
                    .ToList();

                result.Items = result.Items.ToList().Select(s =>
                {
                    s.Action = actionList.FirstOrDefault(f => f.CurrentStatus == s.StatusId);
                    return s;
                }).ToList();
                result.Editable = User.IsInRole("Administrator") || User.IsInRole("ProductionSupervisor");
            }

            return result;
        }

    }
}