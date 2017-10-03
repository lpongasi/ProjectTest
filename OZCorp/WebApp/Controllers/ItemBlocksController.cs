using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using Project.Entities.Block;
using ProjectStart.Common;
using WebApp.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Entities.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class ItemBlocksController : CommonController
    {
        public ItemBlocksController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> role, ApplicationDbContext context, ILoggerFactory loggerFactory, IHostingEnvironment hostingEnv, IOptions<AppSettings> appSettings) : base(userManager, signInManager, role, context, loggerFactory, hostingEnv, appSettings)
        {
        }
        [AllowAnonymous]
        public IActionResult List()
        {
            var blocks = Context
                            .ItemBlocks
                            .ToList()
                            .OrderByDescending(o => o.DateCreated).Select(b =>
                            new
                            {
                                b.Id,
                                b.GroupId,
                                b.ItemUrl,
                                b.Type,
                                b.BackgroundUrl,
                                b.LogoUrl
                            });
            var editable = SignInManager.IsSignedIn(User) && (User.IsInRole("Administrator") || User.IsInRole("ItemManagement"));

            return Json(new { blocks, editable }.ToResponse());
        }
        [Authorize(Roles = "Administrator,ItemManagement")]
        public async Task<IActionResult> CreateBlock(BlockType type)
        {
            var block = new ItemBlock
            {
                GroupId = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now
            };
            switch (type)
            {
                case BlockType.Big:
                    block.Type = "big";
                    break;
                case BlockType.Medium:
                    block.Type = "medium";
                    break;
                case BlockType.Wide:
                    block.Type = "wide";
                    break;
                case BlockType.Tall:
                    block.Type = "tall";
                    break;
                default:
                    block.Type = "medium";
                    break;
            }
            Context.ItemBlocks.Add(block);
            await Context.SaveChangesAsync();
            return Json(block.ToResponse());
        }
        [Authorize(Roles = "Administrator,ItemManagement")]
        public async Task<IActionResult> Update(long id, IList<IFormFile> logo, IList<IFormFile> background)
        {
            var itemBlock = Context.ItemBlocks.SingleOrDefault(w => w.Id == id);
            if (itemBlock == null)
                return List();

            var uploadedLogo = logo.ImageUpload(HostingEnv.WebRootPath, false);
            var uploadedBackground = background.ImageUpload(HostingEnv.WebRootPath, false);
            var removeImages = new List<string>();
            if (uploadedLogo.Any())
            {
                if (!string.IsNullOrEmpty(itemBlock.LogoUrl))
                {
                    removeImages.Add($"{HostingEnv.WebRootPath}{itemBlock.LogoUrl}");
                }
                itemBlock.LogoUrl = uploadedLogo.First().Location;
            }
            if (uploadedBackground.Any())
            {
                if (!string.IsNullOrEmpty(itemBlock.BackgroundUrl))
                {
                    removeImages.Add($"{HostingEnv.WebRootPath}{itemBlock.BackgroundUrl}");
                }
                itemBlock.BackgroundUrl = uploadedBackground.First().Location;
            }
            Context.ItemBlocks.Update(itemBlock);
            await Context.SaveChangesAsync();
            ImageManipulation.Remove(removeImages.ToArray());
            return List();
        }
        [Authorize(Roles = "Administrator,ItemManagement")]
        public async Task<IActionResult> Remove(long id)
        {
            var itemBlock = Context.ItemBlocks.SingleOrDefault(w=>w.Id==id);
            if (itemBlock == null)
                return List();
            var removeFiles = new List<string>();

            if (!string.IsNullOrEmpty(itemBlock.LogoUrl))
                removeFiles.Add($"{HostingEnv.WebRootPath}{itemBlock.LogoUrl}");
            if (!string.IsNullOrEmpty(itemBlock.BackgroundUrl))
                removeFiles.Add($"{HostingEnv.WebRootPath}{itemBlock.BackgroundUrl}");

            Context.ItemBlocks.RemoveRange(itemBlock);
            await Context.SaveChangesAsync();
            ImageManipulation.Remove(removeFiles.ToArray());

            return List();

        }
    }
    public enum BlockType
    {
        Big,
        Medium,
        Wide,
        Tall
    }
}