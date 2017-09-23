using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common.Common;
using Project.Common.Filters;

namespace Project.Common.Extensions
{
    public static class EfExt
    {
        public static async Task<ResponseList<T>> ToResponseAsync<T>(this IQueryable<T> data, CommonFilter filter)
        {
            var listAsync = await data
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            return new ResponseList<T>
            {
                Data = listAsync ?? new List<T>(),
                Page = filter.Page,
                Total = await data.CountAsync(),
                PageSize = filter.PageSize
            };
        }
        public static async Task<ResponseList<T>> ToResponseAsync<T>(this IQueryable<T> data)
        {
            var datalist = await data.ToListAsync();
            return new ResponseList<T>
            {
                Data = datalist ?? new List<T>(),
            };
        }
        public static ResponseList<T> ToResponse<T>(this IQueryable<T> data, CommonFilter filter)
        {
            return new ResponseList<T>
            {
                Data = data
                             ?.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize)
                             ?.ToList() ?? new List<T>(),
                Page = filter.Page,
                Total = data.Count(),
                PageSize = filter.PageSize
            };
        }
        public static ResponseList<T> ToResponse<T>(this IQueryable<T> data)
        {
            return new ResponseList<T>
            {
                Data = data?.ToList() ?? new List<T>(),
            };
        }
    }
}
