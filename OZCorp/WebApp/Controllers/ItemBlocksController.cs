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
            var items = Context
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
            var blocks = items
                            .GroupBy(g => g.GroupId)
                            .Select(s => s.Select(b => b).OrderBy(o => o.Id));
            var slideBlocks = items.Where(w => w.Type.Equals("medium") || w.Type.Equals("big"));
            var editable = SignInManager.IsSignedIn(User) && User.IsInRole("Administrator");

            return Json(new { blocks, editable, slideBlocks }.ToResponse());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateBlock(BlockType type)
        {
            var blocks = new List<ItemBlock>();
            var groupId = Guid.NewGuid().ToString();
            var createDate = DateTime.Now;
            switch (type)
            {
                case BlockType.Big:
                    blocks.Add(new ItemBlock
                    {
                        Type = "big",
                        GroupId = groupId,
                        DateCreated = createDate
                    });
                    break;
                case BlockType.Medium:
                    blocks.AddRange(
                            Enumerable.Range(1, 4).Select(s => new ItemBlock
                            {
                                Type = "medium",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    break;
                case BlockType.Wide:
                    blocks.AddRange(
                            Enumerable.Range(1, 2).Select(s => new ItemBlock
                            {
                                Type = "wide",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    break;
                case BlockType.Tall:
                    blocks.AddRange(
                            Enumerable.Range(1, 2).Select(s => new ItemBlock
                            {
                                Type = "tall",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    break;
                case BlockType.TallMedium:
                    blocks.Add(new ItemBlock
                    {
                        Type = "tall",
                        GroupId = groupId
                    });
                    blocks.AddRange(
                            Enumerable.Range(1, 2).Select(s => new ItemBlock
                            {
                                Type = "medium",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    break;
                case BlockType.WideMedium:
                    blocks.Add(new ItemBlock
                    {
                        Type = "wide",
                        GroupId = groupId,
                        DateCreated = createDate
                    });
                    blocks.AddRange(
                            Enumerable.Range(1, 2).Select(s => new ItemBlock
                            {
                                Type = "medium",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    break;
                case BlockType.MediumWide:
                    blocks.AddRange(
                            Enumerable.Range(1, 2).Select(s => new ItemBlock
                            {
                                Type = "medium",
                                GroupId = groupId,
                                DateCreated = createDate
                            })
                        );
                    blocks.Add(new ItemBlock
                    {
                        Type = "wide",
                        GroupId = groupId,
                        DateCreated = createDate
                    });
                    break;
                default:
                    blocks.Add(new ItemBlock
                    {
                        Type = "big",
                        GroupId = groupId,
                        DateCreated = createDate
                    });
                    break;
            }
            Context.ItemBlocks.AddRange(blocks);
            await Context.SaveChangesAsync();
            return Json(blocks.Select(b =>
                            new
                            {
                                b.Id,
                                b.GroupId,
                                b.ItemUrl,
                                b.Type,
                                b.BackgroundUrl,
                                b.LogoUrl
                            }).OrderBy(o => o.Id).ToResponse());
        }
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(string id)
        {
            var itemBlock = Context.ItemBlocks.Where(w => w.GroupId.Equals(id));
            if (itemBlock == null || !itemBlock.Any())
                return List();
            var removeFiles = new List<string>();

            var removeLogosFiles = itemBlock
                .Where(w => !string.IsNullOrWhiteSpace(w.LogoUrl))
                .Select(s => $"{HostingEnv.WebRootPath}{s.LogoUrl}").ToList();

            var removebackgroundFiles = itemBlock
                .Where(w => !string.IsNullOrWhiteSpace(w.BackgroundUrl))
                .Select(s => $"{HostingEnv.WebRootPath}{s.BackgroundUrl}").ToList();

            if (removeLogosFiles.Any())
                removeFiles.AddRange(removeLogosFiles);
            if (removebackgroundFiles.Any())
                removeFiles.AddRange(removebackgroundFiles);

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
        Tall,
        TallMedium,
        WideMedium,
        MediumWide,
    }
}