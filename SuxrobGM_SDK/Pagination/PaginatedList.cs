using System;
using System.Collections.Generic;
using System.Linq;

namespace SuxrobGM_SDK.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }      

        public PaginatedList(IList<T> items, int count, int pageIndex, int pageSize = 10)
        {
            PageIndex = pageIndex;           
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage { get => PageIndex > 1; }  
        public bool HasNextPage { get => PageIndex < TotalPages; }           

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize = 10)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
