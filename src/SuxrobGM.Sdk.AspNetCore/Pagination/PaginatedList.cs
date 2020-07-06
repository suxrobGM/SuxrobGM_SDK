using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuxrobGM.Sdk.AspNetCore.Pagination
{
    /// <summary>
    /// Class to create paged items 
    /// </summary>
    /// <typeparam name="T">Data type of items</typeparam>
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize = 10)
        {
            PageIndex = pageIndex;           
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize = 10)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize = 10)
        {
            var sourceArray = source as T[] ?? source.ToArray();
            var count = sourceArray.Length;
            var items = sourceArray.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize);
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
