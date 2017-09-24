using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using Project.Entities.Block;
using ProjectStart.Common;

namespace WebApp.Controllers
{
    public class ItemBlocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ItemBlocksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var block = _context
                            .ItemBlocks
                            .ToList()
                            .OrderByDescending(o => o.DateCreated)
                            .GroupBy(g => g.GroupId)
                            .Select(s => s.Select(b =>
                            new {
                               b.Id,
                               b.GroupId,
                               b.ItemUrl,
                               b.Type,
                               b.BackgroundUrl,
                               b.LogoUrl
                            }).OrderBy(o=>o.Id));
            return Json(block.ToResponse());
        }
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
            _context.ItemBlocks.AddRange(blocks);
            await _context.SaveChangesAsync();
            return Json(blocks.Select(b =>
                            new {
                                b.Id,
                                b.GroupId,
                                b.ItemUrl,
                                b.Type,
                                b.BackgroundUrl,
                                b.LogoUrl
                            }).OrderBy(o => o.Id).ToResponse());
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