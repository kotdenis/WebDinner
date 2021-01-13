using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDinner.Models.ViewModels;

namespace WebDinner.Infrastructure
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            TotalPages = (query.Count() + 1) / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    public class QueryOptions
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
