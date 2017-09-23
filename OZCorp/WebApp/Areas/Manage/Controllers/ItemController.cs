using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebApp.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Project.Common.Extensions;
using Project.Entities.Category;
using Project.Entities.Identity;
using WebApp.Common;
using Project.Common.Filters;
using Project.Entities.Product;
using Project.Models.Item;
using WebApp.Extensions;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles = "Administrator,ItemManagement")]
    [Authorize(Policy = Config.MainPolicy)]
    public class ItemController : CommonController<ItemController>
    {
        public ItemController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }

        // GET: Manage/Item
        public IActionResult Index()
        {
            return View();
        }
        // GET: Manage/Item/List
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
                || w.Barcode.Contains(gridFilter.Filter)
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

        // GET: Manage/Item/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            if (item.UomId <= 0)
            {
                ModelState.Remove("UomId");
                ModelState.AddModelError("UomId", "Unit of Measure is Required!");
            }
            if (item.CategoryId <= 0)
            {
                ModelState.Remove("CategoryId");
                ModelState.AddModelError("CategoryId", "Category is Required!");
            }
            if (item.SubCategoryId <= 0)
            {
                ModelState.Remove("SubCategoryId");
                ModelState.AddModelError("SubCategoryId", "Sub Category is Required!");
            }
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    item.ItemHistories = new HashSet<ItemHistory>
                    {
                        new ItemHistory
                        {
                            Action = "Add Item",
                            ActionDate = DateTime.Now,
                            ActionUserId = UserManager.GetUserId(User)
                        }
                    };
                    Context.Add(item);
                    await Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

            }

            return RedirectToAction("Index");
        }

        // GET: Manage/Item/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || !ItemExists(id.Value))
            {
                return RedirectToAction("Index");
            }

            var item = await Context.Item
                            .Include(i => i.Images)
                            .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Manage/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Item item, IList<IFormFile> imageUpload)
        {
            if (id != item.Id || !ItemExists(item.Id))
            {
                return RedirectToAction("Index");
            }
            if (item.UomId <= 0)
            {
                ModelState.Remove("UomId");
                ModelState.AddModelError("UomId", "Unit of Measure is Required!");
            }
            if (item.CategoryId <= 0)
            {
                ModelState.Remove("CategoryId");
                ModelState.AddModelError("CategoryId", "Category is Required!");
            }
            if (item.SubCategoryId <= 0)
            {
                ModelState.Remove("SubCategoryId");
                ModelState.AddModelError("SubCategoryId", "Sub Category is Required!");
            }
            if (ModelState.IsValid)
            {
                var imageLocations = new List<string>();
                var images = imageUpload.ImageUpload(HostingEnv.WebRootPath).Select(s => new ImageLocation
                {
                    ItemId = item.Id,
                    FileName = s.Name,
                    FileLocation = s.Location
                }).ToList();

                if (images.Any())
                {
                    Context.ImageLocations.Where(w => w.ItemId == item.Id).ToList().ForEach(e =>
                    {
                        imageLocations.Add($"{HostingEnv.WebRootPath}{e.FileLocation}");
                        Context.ImageLocations.Remove(e);
                    });
                    Context.ImageLocations.AddRange(images);
                }

                using (var transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        if (item.ItemHistories == null)
                            item.ItemHistories = new List<ItemHistory>();
                        var changes = Context.Item.AsNoTracking().First(f => f.Id == item.Id)
                                .GetChangesFrom(item);
                        item.ItemHistories.Add(new ItemHistory
                        {
                            Action = string.IsNullOrEmpty(changes) ? "Update Item" : changes,
                            ActionDate = DateTime.Now,
                            ActionUserId = UserManager.GetUserId(User)
                        });
                        Context.Update(item);
                        await Context.SaveChangesAsync();
                        transaction.Commit();
                        ImageManipulation.Remove(imageLocations.ToArray());
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Manage/Item/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await Context.Item
                .Include(i => i.Category)
                .ThenInclude(i => i.Categories)
                .Include(i => i.UnitOfMeasure)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Manage/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await Context.Item.SingleOrDefaultAsync(m => m.Id == id);
            item.Removed = true;
            item.NotForSale = true;
            Context.Update(item);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CategoryAdd(string name)
        {
            if (string.IsNullOrEmpty(name) || Context.Category.Any(c => c.Name.Equals(name)))
                return Categories();
            Context.Category.Add(new Category
            {
                Name = name
            });
            Context.SaveChanges();
            return Categories();
        }
        [HttpPost]
        public IActionResult UnitAdd(string name)
        {
            if (string.IsNullOrEmpty(name) || Context.UnitOfMeasure.Any(c => c.Name.Equals(name)))
                return Unit();
            Context.UnitOfMeasure.Add(new UnitOfMeasure
            {
                Name = name.ToUpper()
            });
            Context.SaveChanges();
            return Unit();
        }
        [HttpPost]
        public IActionResult SizeAdd(string name)
        {
            if (string.IsNullOrEmpty(name) || Context.Sizes.Any(c => c.Name.Equals(name)))
                return Size();
            Context.Sizes.Add(new Size
            {
                Name = name.ToUpper()
            });
            Context.SaveChanges();
            return Size();
        }

        [HttpPost]
        public IActionResult SubCategoryAdd(int? id, string name)
        {
            var category = Context.Category.FirstOrDefault(c => c.Id == id);
            if (
                category == null
                || string.IsNullOrEmpty(name)
                || Context.SubCategory.Any(c => c.CategoryId == category.Id && c.Name.Equals(name)))
                return SubCategories(id);
            Context.SubCategory.Add(new SubCategory()
            {
                Name = name,
                CategoryId = category.Id
            });
            Context.SaveChanges();
            return SubCategories(id);
        }
        private bool ItemExists(long id)
        {
            return Context.Item.Any(e => e.Id == id);
        }
    }
}
