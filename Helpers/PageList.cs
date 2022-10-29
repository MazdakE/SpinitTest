using Microsoft.EntityFrameworkCore;
using SpinitTest.Queries;

namespace SpinitTest.Helpers
{
    public class PageList<T> : List<T>
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }


        public PageList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            var calculatedPages = pageSize <= 0
                ? 1
                : pageSize;

            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)calculatedPages);
            AddRange(items);
        }

        public static async Task<PageList<T>> CreateAsync(IEnumerable<T> source, PagedListQuery query)
        {
            var count = source.Count();

            if (query.PageSize == 0)
                query.PageSize = count;

            if (count == 0)
                query.PageSize = 1;

            var items = source.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new PageList<T>(items, count, query.PageNumber, query.PageSize);
        }
    }
}
